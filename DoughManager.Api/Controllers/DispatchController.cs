// Decompiled with JetBrains decompiler
// Type: MoringaBakery.Api.Controllers.DispatchController
// Assembly: MoringaBakery.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B2D6541-2FB5-408C-932B-3F249436BB09
// Assembly location: C:\Users\munas\OneDrive\Desktop\moringaBakery (2)\moringaBakery\MoringaBakery.Api.dll

#nullable enable
using DoughManager.Services.Dtos;
using DoughManager.Services.Interfaces;
using DoughManager.Services.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DoughManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DispatchController(IDispatchService dispatchService) : BaseController
{
  [HttpPost("create")]
  public async Task<ActionResult<ServiceResponse<DispatchDto>>> CreateAsync(
    [FromBody] DispatchDto request)
  {
    DispatchController dispatchController = this;
    ServiceResponse<DispatchDto> async = await dispatchService.CreateAsync(request, dispatchController.Account);
    return (ActionResult<ServiceResponse<DispatchDto>>) (ActionResult) dispatchController.Ok((object) async);
  }

  [HttpGet("getByDate")]
  public async Task<ActionResult<ServiceResponse<List<DispatchDto>>>> GetByDateAsync(
    [FromQuery] DateTime start,
    [FromQuery] DateTime end)
  {
    DispatchController dispatchController = this;
    ServiceResponse<List<DispatchDto>> dispatchedItemsAsync = await dispatchService.GetDispatchedItemsAsync(start, end);
    return (ActionResult<ServiceResponse<List<DispatchDto>>>) (ActionResult) dispatchController.Ok((object) dispatchedItemsAsync);
  }
  
  [HttpGet("getReceivedByDate")]
  public async Task<ActionResult<ServiceResponse<List<DispatchDto>>>> GetReceievedByDateAsync(
    [FromQuery] DateTime start,
    [FromQuery] DateTime end)
  {
    var response = await dispatchService.GetReceivedItemsAsync(start, end);
    return Ok(response);
  }
  
  [HttpGet("pendingReceival")]
  public async Task<ActionResult<ServiceResponse<List<DispatchDto>>>> PendingAsync()
  {
    var response = await dispatchService.PendingReceivalAsync();
    return Ok(response);
  }
  
  [HttpPost("receive")]
  public async Task<ActionResult<ServiceResponse<DispatchDto>>> ReceiveAsync([FromBody] DispatchDto request)
  {
    var response = await dispatchService.ReceiveAsync(request, Account);
    return Ok(response);
  }
  
}
