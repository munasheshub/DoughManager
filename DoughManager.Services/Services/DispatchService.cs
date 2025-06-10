// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Services.DispatchService
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using AutoMapper;
using DoughManager.Services.Dtos;
using DoughManager.Services.Interfaces;
using DoughManager.Services.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using DoughManager.Data.DbContexts;
using DoughManager.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DoughManager.Data.Shared;

#nullable enable
namespace DoughManager.Services.Services;

public class DispatchService(
  DoughManagerDbContext context,
  ILogger<DispatchService> logger,
  IMapper mapper) : IDispatchService
{
  public async Task<ServiceResponse<DispatchDto>> CreateAsync(
    DispatchDto dispatch,
    Account account)
  {
    await using var transaction = await context.Database.BeginTransactionAsync();
      try
      {
        
        var newDispatch = mapper.Map<Dispatch>( dispatch);
        newDispatch.DispatchedBy = account.FullName;
        newDispatch.PrepareEntityForCreation(account);
        await context.Dispatches.AddAsync(newDispatch);
        await context.SaveChangesAsync();
        
        var order = await context.Orders
          .AsNoTracking()
          .FirstOrDefaultAsync(o => o.Id == newDispatch.OrderId);
        order.IsDispatched = true;
        order.OnHold = false;
        context.Orders.Update(order);
        await context.SaveChangesAsync();
        
        await transaction.CommitAsync();
        return ServiceResponse<DispatchDto>.Success(dispatch, "Dispatch saved successfully");
      }
      catch (Exception ex)
      {
      
        logger.LogError("Error saving dispatch: " + ex.Message);
        return ServiceResponse<DispatchDto>.Failure(ex.Message);
      }
    
    
  }

  public async Task<ServiceResponse<DispatchDto>> ReceiveAsync(DispatchDto dispatchItem, Account account)
  {
    await using var transaction = await context.Database.BeginTransactionAsync();
    try
    {
      var receivedBatch = mapper.Map<Dispatch>(dispatchItem);
      receivedBatch.IsReceived = true;
      foreach (var item in receivedBatch.DispatchItems)
      {
        item.ReceivedBy = account.FullName;
        item.ReceivedDate = DateTime.Now;
        
      }
      receivedBatch.PrepareEntityForUpdate(account);
      context.Dispatches.Update(receivedBatch);
      await context.SaveChangesAsync();
      
      await transaction.CommitAsync();
      return ServiceResponse<DispatchDto>.Success(dispatchItem, "Dispatch saved successfully");
    }
    catch (Exception ex)
    {
      logger.LogError("Error saving dispatch: " + ex.Message);
      return ServiceResponse<DispatchDto>.Failure(ex.Message);
    }
  }

  public async Task<ServiceResponse<List<DispatchDto>>> GetDispatchedItemsAsync(
    DateTime startDate,
    DateTime endDate)
  {
    try
    {
      var dispatches = await context.Dispatches
        .Where(d => d.CreationTime >= startDate && d.CreationTime <= endDate)
        .Include(d => d.DispatchItems)
        .ThenInclude(di => di.Product)
        .AsNoTracking()
        .ToListAsync();
      if (endDate < startDate) return ServiceResponse<List<DispatchDto>>.Failure("End date must be after start date.");
      return ServiceResponse<List<DispatchDto>>.Success(mapper.Map<List<DispatchDto>>(dispatches), "Dispatches retrieved successfully.");
    }
    catch (Exception ex)
    {
      logger.LogError("Error retrieving dispatched items: " + ex.Message);
      return ServiceResponse<List<DispatchDto>>.Failure(ex.Message);
    }
  }

  public async Task<ServiceResponse<List<DispatchDto>>> GetReceivedItemsAsync(DateTime startDate, DateTime endDate)
  {
    try
    {
      var end = new DateTime(endDate.Year, endDate.Month, endDate.Day, 23, 59, 59, 999);
      var dispatches = await context.Dispatches
        .Where(d => (d.CreationTime >= startDate.Date && d.CreationTime <= end) && d.IsReceived)
        .Include(d => d.DispatchItems)
        .ThenInclude(di => di.Product)
        .AsNoTracking()
        .ToListAsync();
      if (endDate < startDate) return ServiceResponse<List<DispatchDto>>.Failure("End date must be after start date.");
      return ServiceResponse<List<DispatchDto>>.Success(mapper.Map<List<DispatchDto>>(dispatches), "Dispatches retrieved successfully.");
    }
    catch (Exception ex)
    {
      logger.LogError("Error retrieving dispatched items: " + ex.Message);
      return ServiceResponse<List<DispatchDto>>.Failure(ex.Message);
    }
  }

  public async Task<ServiceResponse<List<DispatchDto>>> PendingReceivalAsync()
  {
    try
    {
      var dispatches = await context.Dispatches
        .Include(d => d.Order)
        .Include(d => d.DispatchItems)
          .ThenInclude(di => di.Product)
        .Where(d => d.Order.Location == OrderLocation.Front && d.CreationTime.Value.Date == DateTime.Now.Date)
        .ToListAsync();
      
      if (dispatches.Count == 0) return ServiceResponse<List<DispatchDto>>.Success([]);
      var response = mapper.Map<List<DispatchDto>>(dispatches);
      return ServiceResponse<List<DispatchDto>>.Success(response);

    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      throw;
    }
  }
}
