// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Services.OrderService
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using AutoMapper;
using DoughManager.Services.Dtos.Production;
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

#nullable enable
namespace DoughManager.Services.Services;

public class OrderService(
  DoughManagerDbContext context,
  ILogger<OrderService> logger,
  IMapper mapper) : IOrderService
{
  public async Task<ServiceResponse<OrderDto>> CreateOrderAsync(OrderDto request, Account account)
  {
    try
    {
      Order entity = mapper.Map<Order>( request);
      entity.PrepareEntityForCreation(account);
      EntityEntry<Order> Order = await context.Orders.AddAsync(entity);
      int num = await context.SaveChangesAsync();
      return ServiceResponse<OrderDto>.Success(mapper.Map<OrderDto>((object) await context.Orders.Include(o => o.ProductOrders).FirstOrDefaultAsync<Order>((Expression<Func<Order, bool>>) (p => p.Id == Order.Entity.Id))), "Order created successfully");
    }
    catch (Exception ex)
    {
      logger.LogError("Error creating Order: " + ex.Message);
      return ServiceResponse<OrderDto>.Failure(ex.Message);
    }
  }

  public async Task<ServiceResponse<OrderDto>> UpdateOrderAsync(OrderDto request, Account account)
  {
    try
    {
      Order destination = await context.Orders.AsNoTracking<Order>().FirstOrDefaultAsync<Order>((Expression<Func<Order, bool>>) (p => p.Id == request.Id));
      if (destination == null)
        return ServiceResponse<OrderDto>.Failure("Order not found");
      Order entity = mapper.Map<OrderDto, Order>(request, destination);
      entity.PrepareEntityForUpdate(account);
      context.Orders.Update(entity);
      int num = await context.SaveChangesAsync();
      return ServiceResponse<OrderDto>.Success(mapper.Map<OrderDto>((object) await context.Orders.Include(o => o.ProductOrders).FirstOrDefaultAsync<Order>((Expression<Func<Order, bool>>) (p => p.Id == request.Id))), "Order updated successfully");
    }
    catch (Exception ex)
    {
      logger.LogError("Error updating Order: " + ex.Message);
      return ServiceResponse<OrderDto>.Failure(ex.Message);
    }
  }

  public async Task<ServiceResponse<List<OrderDto>>> GetAllOrderAsync()
  {
    try
    {
      List<Order> listAsync = await context.Orders
        .Include(o => o.ProductOrders)
         .ThenInclude(po => po.Product)
        .AsNoTracking()
        .ToListAsync();
      return listAsync == null || !listAsync.Any() ? ServiceResponse<List<OrderDto>>.Failure("No Orders found") : ServiceResponse<List<OrderDto>>.Success(mapper.Map<List<OrderDto>>( listAsync), "Orders retrieved successfully");
    }
    catch (Exception ex)
    {
      logger.LogError("Error retrieving Orders: " + ex.Message);
      return ServiceResponse<List<OrderDto>>.Failure(ex.Message);
    }
  }

  public async Task<ServiceResponse<List<OrderDto>>> GetAllPendingOrderAsync()
  {
    try
    {
      List<Order> listAsync = await context.Orders.Include(o => o.ProductOrders).ThenInclude(po => po.Product).Where<Order>((Expression<Func<Order, bool>>) (o => !o.IsInProduction && !o.IsDispatched)).AsNoTracking<Order>().ToListAsync<Order>();
      return listAsync == null || !listAsync.Any() ? ServiceResponse<List<OrderDto>>.Success([]) : ServiceResponse<List<OrderDto>>.Success(mapper.Map<List<OrderDto>>( listAsync), "Orders retrieved successfully");
    }
    catch (Exception ex)
    {
      logger.LogError("Error retrieving Orders: " + ex.Message);
      return ServiceResponse<List<OrderDto>>.Failure(ex.Message);
    }
  }
}
