// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Services.ProductionBatchService
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using AutoMapper;
using DoughManager.Services.Dtos.Production;
using DoughManager.Services.Interfaces;
using DoughManager.Services.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using DoughManager.Data.DbContexts;
using DoughManager.Data.EntityModels;
using DoughManager.Data.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text.Json;
using System.Threading.Tasks;
using ProductStatus = DoughManager.Services.Interfaces.ProductStatus;

#nullable enable
namespace DoughManager.Services.Services;

public class ProductionBatchService(
  DoughManagerDbContext context,
  ILogger<ProductionBatchService> logger,
  IMapper mapper) : IProductionBatchService
{
  public async Task<ServiceResponse<ProductionBatchDto>> AddProductAsync(ProductBatchDto request, Account account)
  {
    await using var transaction = await context.Database.BeginTransactionAsync();
    
    {

      try
      {
        
        var newBatch = mapper.Map<ProductBatch>(request);
        
        var batch = await context.ProductBatches.AddAsync(newBatch);
        await context.SaveChangesAsync();
        
        var rawMaterials = await context.RawMaterials
          .Where(rm => request.RawMaterialsUsed.Select(r => r.RawMaterialId).Contains(rm.Id))
          .AsNoTracking()
          .ToListAsync();
        var updatedRawMaterials = UpdateRawMaterialsUsed(newBatch.RawMaterialsUsed.ToList(), rawMaterials);
        context.RawMaterials.UpdateRange(updatedRawMaterials); 
        await context.SaveChangesAsync();
        await transaction.CommitAsync();
        var response = await context.ProductionBatches
          .Include(pb => pb.ProductOrders)
          .Include(pb => pb.Creator)
          .Include(pd => pd.ProductBatches)
          .ThenInclude(pdd => pdd.Product)
          .Include(pd => pd.ProductBatches)
          .ThenInclude(pb => pb.RawMaterialsUsed)
          .ThenInclude(rm => rm.RawMaterial)
          .AsNoTracking()
          .FirstOrDefaultAsync(pd => pd.Id == request.ProductionBatchId);
        return ServiceResponse<ProductionBatchDto>.Success(mapper.Map<ProductionBatchDto>(response));

      }
      catch (Exception ex)
      {

        await transaction.RollbackAsync();
        return ServiceResponse<ProductionBatchDto>.Failure("Failed to create production batch.");
      }



      static List<RawMaterial> UpdateRawMaterialsUsed(
        List<ProductBatchRawMaterial> rawMaterialsUsed,
        List<RawMaterial> existingRawMaterialsUsed)
      {
        foreach (ProductBatchRawMaterial batchRawMaterial in rawMaterialsUsed)
        {
          ProductBatchRawMaterial rawMaterial = batchRawMaterial;
          existingRawMaterialsUsed.FirstOrDefault(rm => rm.Id == rawMaterial.RawMaterialId).QuantityOnHand -=
            rawMaterial.QuantityUsed;
        }

        return existingRawMaterialsUsed;
      }
    }
  }

  public async Task<ServiceResponse<List<ProductionBatchDto>>> GetAllAsync(DateTime start, DateTime end)
  {
    try
    {
      var productionBatches = await context.ProductionBatches
        .Include(pb => pb.ProductOrders)
        .Include(pb => pb.Creator)
        .Include(pd => pd.ProductBatches)
          .ThenInclude(pdd => pdd.Product)
        .Include(pd => pd.ProductBatches)
          .ThenInclude(pb => pb.RawMaterialsUsed)
              .ThenInclude(rm => rm.RawMaterial)
        .Where(pb => pb.CreationTime.Value >= start && pb.CreationTime.Value <= end)
        .OrderByDescending(pb => pb.CreationTime)
        .AsNoTracking()
        .ToListAsync();
      return ServiceResponse<List<ProductionBatchDto>>.Success(mapper.Map<List<ProductionBatchDto>>(
        productionBatches));
    }
    catch (Exception ex)
    {
      return ServiceResponse<List<ProductionBatchDto>>.Failure("Failed to retrieve production batches.");
    }
  }

  public async Task<ServiceResponse<ProductionBatchDto>> CreateAsync(
    ProductionBatchDto request,
    Account account)
  {

    await using var transaction = await context.Database.BeginTransactionAsync();
    
    {

      try
      {
        
        var newBatch = mapper.Map<ProductionBatch>(request);
        
        var batch = await context.ProductionBatches.AddAsync(newBatch);
        await context.SaveChangesAsync();
        if (request.Orders.Count > 0)
        {

          var productOrders = await context.ProductOrders
            .Where(o => request.Orders.Contains(o.Id))
            .AsNoTracking()
            .ToListAsync();
          
          foreach (var productOrder in productOrders)
          {
            productOrder.ProductionBatchId = batch.Entity.Id;
            productOrder.IsInProduction = true;
          }

          context.ProductOrders.UpdateRange(productOrders);
          await context.SaveChangesAsync();
          
          var orders = await context.Orders.Where(o => request.Orders.Contains(o.Id)).ToListAsync();
          foreach (var order in orders)
          {
            order.IsInProduction = true;
          }
          
          context.Orders.UpdateRange(orders);
          await context.SaveChangesAsync();
          
        }

        var rawMaterialsUsed = new List<ProductBatchRawMaterial>();
        foreach (var productBatch in newBatch.ProductBatches) {
          rawMaterialsUsed.AddRange(productBatch.RawMaterialsUsed);
        }
        var rawMaterials = await context.RawMaterials
          .Where(rm => rawMaterialsUsed.Select(r => r.RawMaterialId).Contains(rm.Id))
          .AsNoTracking()
          .ToListAsync();
        var updatedRawMaterials = UpdateRawMaterialsUsed(rawMaterialsUsed, rawMaterials);
        context.RawMaterials.UpdateRange(updatedRawMaterials); 
        await context.SaveChangesAsync();
        await transaction.CommitAsync();
        return ServiceResponse<ProductionBatchDto>.Success(mapper.Map<ProductionBatchDto>((object)batch.Entity));

      }
      catch (Exception ex)
      {

        await transaction.RollbackAsync();
        return ServiceResponse<ProductionBatchDto>.Failure("Failed to create production batch.");
      }



      static List<RawMaterial> UpdateRawMaterialsUsed(
        List<ProductBatchRawMaterial> rawMaterialsUsed,
        List<RawMaterial> existingRawMaterialsUsed)
      {
        foreach (ProductBatchRawMaterial batchRawMaterial in rawMaterialsUsed)
        {
          ProductBatchRawMaterial rawMaterial = batchRawMaterial;
          existingRawMaterialsUsed.FirstOrDefault(rm => rm.Id == rawMaterial.RawMaterialId).QuantityOnHand -=
            rawMaterial.QuantityUsed;
        }

        return existingRawMaterialsUsed;
      }
    }
  }



  private async Task UpdateOrders(int batchId)
  {
   var orders = await context.Orders
      .Where(o => o.ProductOrders.Any(p => p.ProductionBatchId == batchId))
      .AsNoTracking()
      .ToListAsync();
    List<Order> updatedOrders = new List<Order>();
    if (orders.Count > 0)
    {
      foreach (Order order in orders)
      {
        if (await context.Orders.AllAsync(o =>
              o.ProductOrders.All(p => p.IsFinished)))
        {
          order.OnHold = true;
          order.IsInProduction = false;
          updatedOrders.Add(order);
        }
      }
    }

    if (updatedOrders.Count <= 0)
    {
      updatedOrders = null;
    }
    else
    {
      context.Orders.UpdateRange(updatedOrders);
      await context.SaveChangesAsync();
    }
  }

  private async Task UpdateProductOrders(int batchId, int productId)
  {
    List<ProductOrder> listAsync = await context.ProductOrders
      .Where(o => o.ProductionBatchId == batchId && o.ProductId == productId)
      .AsNoTracking()
      .ToListAsync();
    if (listAsync.Count <= 0)
      return;
    foreach (ProductOrder productOrder in listAsync)
    {
      productOrder.IsInProduction = false;
      productOrder.IsFinished = true;
    }

    context.ProductOrders.UpdateRange(listAsync);
    await context.SaveChangesAsync();
  }

  public async Task<ServiceResponse<ProductBatchDto>> UpdateProductStatusAsync(ProductBatchDto productStatus)
  {
    await using var transaction = await context.Database.BeginTransactionAsync();
    try
    {
      var productBatch = await context.ProductBatches
        .AsNoTracking()
        .FirstOrDefaultAsync(pd =>
          pd.Id == productStatus.Id && pd.ProductionBatchId == productStatus.ProductionBatchId);
      if (productBatch == null) return ServiceResponse<ProductBatchDto>.Failure("Product batch not found.");
      var updatedBatch = mapper.Map<ProductBatch>(productStatus);
      productBatch = mapper.Map(updatedBatch, productBatch);
      productBatch.Status = productStatus.Status;
      var processes = JsonSerializer.Deserialize<List<ProductionProcessDto>>(productBatch.ProductionProcesses);
      processes.LastOrDefault().EndTime = DateTime.Now;
      var newProcess = new ProductionProcessDto
      {
        StartTime = DateTime.Now,
        EndTime = (productStatus.Status == ProductionStatus.Completed || productStatus.Status == ProductionStatus.Failed ) ? DateTime.Now : null,
        ProcessName = productStatus.Status
      };
      processes.Add(newProcess);
      var serializedProcess = JsonSerializer.Serialize(processes);
      productBatch.ProductionProcesses = serializedProcess;
      if(productBatch.Status == ProductionStatus.Completed || productBatch.Status == ProductionStatus.Failed) productBatch.EndTime = DateTime.Now;
      if (productBatch.Status == ProductionStatus.Completed)
      {
        await UpdateProductOrders(productBatch.ProductionBatchId, productBatch.ProductId);
      }
      context.ProductBatches.UpdateRange(productBatch);
      await context.SaveChangesAsync();
      var productionBatch = await context.ProductionBatches
        .AsNoTracking()
        .FirstOrDefaultAsync(pb => pb.Id == productStatus.ProductionBatchId);
      var isCompleted = await context.ProductBatches
        .AsNoTracking()
        .Where(pb => pb.ProductionBatchId == productStatus.ProductionBatchId)
        .AllAsync(pb => pb.Status == ProductionStatus.Completed || pb.Status == ProductionStatus.Failed);;
      
      if (isCompleted)
      {
        productionBatch.ClosingGas = productStatus.ClosingGas;
        productionBatch.ClosingZesaUnits = productStatus.ClosingZesaUnits;
        productionBatch.Status = ProductionStatus.Completed;
        productionBatch.EndTime = DateTime.Now;
        await UpdateOrders(productionBatch.Id);
        context.ProductionBatches.Update(productionBatch);
        await context.SaveChangesAsync();
      }
      await transaction.CommitAsync();
      var response = await context.ProductBatches
        .Include(pb => pb.RawMaterialsUsed)
        .ThenInclude(rm => rm.RawMaterial)
        .AsNoTracking()
        .FirstOrDefaultAsync(pb => pb.Id == productStatus.Id);
      return ServiceResponse<ProductBatchDto>.Success(mapper.Map<ProductBatchDto>(response));

    }
    catch (Exception e)
    {
      await transaction.RollbackAsync();
      return ServiceResponse<ProductBatchDto>.Failure("Failed to update quantity.");
    }
  }

  public async Task<ServiceResponse<ProductionBatchDto>> UpdateQuantityAsync(
    int quantity,
    int batchId,
    Account account)
  {
    try
    {
      ProductionBatch entity = await context.ProductionBatches.AsNoTracking<ProductionBatch>()
        .FirstOrDefaultAsync<ProductionBatch>((Expression<Func<ProductionBatch, bool>>)(pb => pb.Id == batchId));
      if (entity == null)
        return ServiceResponse<ProductionBatchDto>.Failure("Batch not found");
      entity.QuantityToBeProduced = quantity;
      entity.PrepareEntityForUpdate(account);
      context.ProductionBatches.Update(entity);
      int num = await context.SaveChangesAsync();
      return ServiceResponse<ProductionBatchDto>.Success(mapper.Map<ProductionBatchDto>((object)await context
        .ProductionBatches
        .Include(pb => pb.ProductOrders)
        .AsNoTracking()
        .FirstOrDefaultAsync(pb => pb.Id == batchId)));
    }
    catch (Exception ex)
    {
      return ServiceResponse<ProductionBatchDto>.Failure("Failed to update quantity.");
    }
  }

  public async Task<ServiceResponse<ProductionBatchDto>> UpdateStatusAsync(
    UpdateProductionBatchStatus newStatus,
    Account account)
  {
    int num = 0;
    ServiceResponse<ProductionBatchDto> serviceResponse;
    await using (IDbContextTransaction transaction = await context.Database.BeginTransactionAsync())
    {
      int num1;
      try
      {
        ProductionBatch productionBatch = await context.ProductionBatches
          .Include(pb => pb.ProductOrders)
          .AsNoTracking()
          .FirstOrDefaultAsync(pb => pb.Id == newStatus.BatchId);
        if (productionBatch == null)
        {
          serviceResponse = ServiceResponse<ProductionBatchDto>.Failure("Production batch not found.");
        }

        ProductionProcessDto productionProcessDto = new ProductionProcessDto()
        {
          ProcessName = newStatus.Status,
          StartTime = DateTime.Now,
          EndTime = new DateTime?(),
          Notes = ""
        };
        ProductionBatchDto source = mapper.Map<ProductionBatchDto>((object)productionBatch);
        if (source.ProductionProcesses.Count > 0)
          source.ProductionProcesses.LastOrDefault().EndTime = new DateTime?(DateTime.Now);
        else
          source.ProductionProcesses = new List<ProductionProcessDto>();
        source.ProductionProcesses.Add(productionProcessDto);
        source.Status = newStatus.Status;
        ProductionBatch updatedBatch = mapper.Map<ProductionBatch>(source);
        if (updatedBatch.Status == ProductionStatus.Completed)
        {
          Product entity = await context.Products.AsNoTracking<Product>()
            .FirstOrDefaultAsync<Product>((Expression<Func<Product, bool>>)(p => p.Id == updatedBatch.ProductOrders.FirstOrDefault().Id));
          entity.QuantityOnHand += (double)updatedBatch.QuantityToBeProduced;
          context.Products.Update(entity);
          int num2 = await context.SaveChangesAsync();
          //await this.UpdateProductOrders(updatedBatch.Id);
          await this.UpdateOrders(updatedBatch.Id);
        }

        updatedBatch.PrepareEntityForUpdate(account);
        context.ProductionBatches.Update(updatedBatch);
        await context.SaveChangesAsync();
        await transaction.CommitAsync();
        serviceResponse =
          ServiceResponse<ProductionBatchDto>.Success(mapper.Map<ProductionBatchDto>((object)productionBatch));

      }
      catch (Exception ex)
      {

        await transaction.RollbackAsync();
        return ServiceResponse<ProductionBatchDto>.Failure("Failed to update production batch status.");

      }


    }

    return null;
  }

}