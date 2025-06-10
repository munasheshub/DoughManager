// Decompiled with JetBrains decompiler
// Type: MoringaBakery.Api.Controllers.OrderController
// Assembly: MoringaBakery.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B2D6541-2FB5-408C-932B-3F249436BB09
// Assembly location: C:\Users\munas\OneDrive\Desktop\moringaBakery (2)\moringaBakery\MoringaBakery.Api.dll

#nullable enable
using DoughManager.Services.Dtos.Production;
using DoughManager.Services.Interfaces;
using DoughManager.Services.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DoughManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(IOrderService orderService) : BaseController
{
  [HttpPost("create")]
  public async Task<ActionResult<ServiceResponse<OrderDto>>> CreateAsync(OrderDto request)
  {
    OrderController orderController = this;
    ServiceResponse<OrderDto> orderAsync = await orderService.CreateOrderAsync(request, orderController.Account);
    return (ActionResult<ServiceResponse<OrderDto>>) (ActionResult) orderController.Ok((object) orderAsync);
  }

  [HttpPut("update")]
  public async Task<ActionResult<ServiceResponse<OrderDto>>> UpdateAsync(OrderDto request)
  {
    OrderController orderController = this;
    ServiceResponse<OrderDto> serviceResponse = await orderService.UpdateOrderAsync(request, orderController.Account);
    return (ActionResult<ServiceResponse<OrderDto>>) (ActionResult) orderController.Ok((object) serviceResponse);
  }

  [HttpGet("getAll")]
  public async Task<ActionResult<ServiceResponse<List<OrderDto>>>> GetAllAsync()
  {
    OrderController orderController = this;
    ServiceResponse<List<OrderDto>> allOrderAsync = await orderService.GetAllOrderAsync();
    return (ActionResult<ServiceResponse<List<OrderDto>>>) (ActionResult) orderController.Ok((object) allOrderAsync);
  }

  [HttpGet("getAllPending")]
  public async Task<ActionResult<ServiceResponse<List<OrderDto>>>> GetAllPendingAsync()
  {
    OrderController orderController = this;
    ServiceResponse<List<OrderDto>> pendingOrderAsync = await orderService.GetAllPendingOrderAsync();
    return (ActionResult<ServiceResponse<List<OrderDto>>>) (ActionResult) orderController.Ok((object) pendingOrderAsync);
  }
}
