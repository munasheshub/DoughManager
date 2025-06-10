// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Services.RawMaterialService
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using AutoMapper;
using DoughManager.Services.Dtos.Product;
using DoughManager.Services.Interfaces;
using DoughManager.Services.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using DoughManager.Data.DbContexts;
using DoughManager.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DoughManager.Services.Dtos.RawMaterial;

#nullable enable
namespace DoughManager.Services.Services;

public class RawMaterialService(
  DoughManagerDbContext context,
  ILogger<ProductService> logger,
  IMapper mapper) : IRawMaterialService
{
  public async Task<ServiceResponse<CreateRawMaterialRequest>> CreateRawMaterialAsync(
    CreateRawMaterialRequest request)
  {
    try
    {
      EntityEntry<RawMaterial> rawMaterial = await context.RawMaterials.AddAsync(mapper.Map<RawMaterial>( request));
      int num = await context.SaveChangesAsync();
      return ServiceResponse<CreateRawMaterialRequest>.Success(mapper.Map<CreateRawMaterialRequest>((object) await context.RawMaterials.FirstOrDefaultAsync<RawMaterial>((Expression<Func<RawMaterial, bool>>) (rm => rm.Id == rawMaterial.Entity.Id))), "Raw Material created successfully");
    }
    catch (Exception ex)
    {
      logger.LogError("Error creating raw material: " + ex.Message);
      return ServiceResponse<CreateRawMaterialRequest>.Failure(ex.Message);
    }
  }

  public async Task<ServiceResponse<CreateRawMaterialRequest>> UpdateRawMaterialAsync(
    CreateRawMaterialRequest request)
  {
    try
    {
      RawMaterial destination = await context.RawMaterials.AsNoTracking<RawMaterial>().FirstOrDefaultAsync<RawMaterial>((Expression<Func<RawMaterial, bool>>) (p => p.Id == request.Id));
      if (destination == null)
        return ServiceResponse<CreateRawMaterialRequest>.Failure("Raw Material not found");
      context.RawMaterials.Update(mapper.Map<CreateRawMaterialRequest, RawMaterial>(request, destination));
      int num = await context.SaveChangesAsync();
      return ServiceResponse<CreateRawMaterialRequest>.Success(mapper.Map<CreateRawMaterialRequest>((object) await context.RawMaterials.FirstOrDefaultAsync<RawMaterial>((Expression<Func<RawMaterial, bool>>) (rm => rm.Id == request.Id))), "Raw Material updated successfully");
    }
    catch (Exception ex)
    {
      logger.LogError("Error updating raw material: " + ex.Message);
      return ServiceResponse<CreateRawMaterialRequest>.Failure(ex.Message);
    }
  }

  public async Task<ServiceResponse<bool>> AddStockAsync(int rawmaterialId, decimal quantity, Account account)
  {
    await using var transaction = await context.Database.BeginTransactionAsync();
    try
    {
      var material = await context.RawMaterials.FirstOrDefaultAsync(rm => rm.Id == rawmaterialId);
      if (material == null) return ServiceResponse<bool>.Failure("Raw Material not found");
      material.QuantityOnHand += quantity;
      context.RawMaterials.Update(material);
      await context.SaveChangesAsync();

      var newStock = new RawMaterialInventory
      {
        RawMaterialId = rawmaterialId,
        Quantity = (double)quantity,
        
      };
      newStock.PrepareEntityForCreation(account);
      
      await context.AddAsync(newStock);
      await context.SaveChangesAsync();
      await transaction.CommitAsync();
      return ServiceResponse<bool>.Success(true);

    }
    catch (Exception ex)
    {
      await transaction.RollbackAsync();
      logger.LogError("Error updating raw material: " + ex.Message);
      return ServiceResponse<bool>.Failure(ex.Message);
    }
  }

  public async Task<ServiceResponse<List<InventoryLog>>> GetInventoryAsync(DateTime start, DateTime end)
  {
    try
    {
      var endDate =  new DateTime(end.Year, end.Month, end.Day, 23, 59, 59);
      var stocks = await context.RawMaterialInventories
        .Include(rmi => rmi.Creator)
        .Include(rmi => rmi.RawMaterial)
        .Where(rmi => rmi.CreationTime >= start.Date && rmi.CreationTime <= endDate )
        .ToListAsync();
   
      if (stocks.Count > 0)
      {
        var response = mapper.Map<List<InventoryLog>>(stocks);
        return ServiceResponse<List<InventoryLog>>.Success(response);
      }
      else
      {
        return ServiceResponse<List<InventoryLog>>.Success(null);
      }
      
    }
    catch (Exception ex)
    {
      logger.LogError("Error retrieving raw materials: " + ex.Message);
      return ServiceResponse<List<InventoryLog>>.Failure(ex.Message);
    }
  }

  public async Task<ServiceResponse<bool>> DeleteAsync(int rawmaterial)
  {
    try
    {
      var material = await context.RawMaterials.FirstOrDefaultAsync(rm => rm.Id == rawmaterial);
      if (material == null) return ServiceResponse<bool>.Failure("Raw Material not found");
      material.IsDeleted = true;
      context.RawMaterials.Update(material);
      await context.SaveChangesAsync();
      return ServiceResponse<bool>.Success(true);
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      throw;
    }
  }

  public async Task<ServiceResponse<List<CreateRawMaterialRequest>>> GetAllAsync()
  {
    try
    {
      List<RawMaterial> listAsync = await context.RawMaterials.AsNoTracking<RawMaterial>().ToListAsync<RawMaterial>();
      return listAsync == null || !listAsync.Any() ? ServiceResponse<List<CreateRawMaterialRequest>>.Failure("No raw materials found") : ServiceResponse<List<CreateRawMaterialRequest>>.Success(mapper.Map<List<CreateRawMaterialRequest>>( listAsync), "Raw materials retrieved successfully");
    }
    catch (Exception ex)
    {
      logger.LogError("Error retrieving raw materials: " + ex.Message);
      return ServiceResponse<List<CreateRawMaterialRequest>>.Failure(ex.Message);
    }
  }
}
