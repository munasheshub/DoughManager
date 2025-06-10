// Decompiled with JetBrains decompiler
// Type: MoringaBakery.Api.Controllers.RawMaterialController
// Assembly: MoringaBakery.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B2D6541-2FB5-408C-932B-3F249436BB09
// Assembly location: C:\Users\munas\OneDrive\Desktop\moringaBakery (2)\moringaBakery\MoringaBakery.Api.dll

#nullable enable
using DoughManager.Services.Dtos.Product;
using DoughManager.Services.Interfaces;
using DoughManager.Services.Shared;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DoughManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RawMaterialController(IRawMaterialService rawMaterialService) : BaseController
{
  [HttpPost("create")]
  public async Task<ActionResult<ServiceResponse<CreateRawMaterialRequest>>> CreateAsync(
    CreateRawMaterialRequest request)
  {
    RawMaterialController materialController = this;
    ServiceResponse<CreateRawMaterialRequest> rawMaterialAsync = await rawMaterialService.CreateRawMaterialAsync(request);
    return (ActionResult<ServiceResponse<CreateRawMaterialRequest>>) (ActionResult) materialController.Ok((object) rawMaterialAsync);
  }

  [HttpPut("update")]
  public async Task<ActionResult<ServiceResponse<CreateRawMaterialRequest>>> UpdateAsync(
    CreateRawMaterialRequest request)
  {
    RawMaterialController materialController = this;
    ServiceResponse<CreateRawMaterialRequest> serviceResponse = await rawMaterialService.UpdateRawMaterialAsync(request);
    return (ActionResult<ServiceResponse<CreateRawMaterialRequest>>) (ActionResult) materialController.Ok((object) serviceResponse);
  }

  [HttpGet("getAll")]
  public async Task<ActionResult<ServiceResponse<List<CreateRawMaterialRequest>>>> GetAllAsync()
  {
    RawMaterialController materialController = this;
    ServiceResponse<List<CreateRawMaterialRequest>> allAsync = await rawMaterialService.GetAllAsync();
    return (ActionResult<ServiceResponse<List<CreateRawMaterialRequest>>>) (ActionResult) materialController.Ok((object) allAsync);
  }
  [HttpGet("addStock")]
  public async Task<ActionResult<ServiceResponse<bool>>> AddStockAsync([FromQuery] int materialId, [FromQuery]  decimal quantity)
  {
    var response = await rawMaterialService.AddStockAsync(materialId, quantity, Account);
    return Ok(response);
  }
  
  [HttpGet("delete/{materialId:int}")]
  public async Task<ActionResult<ServiceResponse<bool>>> DeleteAsync(int materialId)
  {
    var response = await rawMaterialService.DeleteAsync(materialId);
    return Ok(response);
  }
  
  [HttpGet("getStock")]
  public async Task<ActionResult<ServiceResponse<bool>>> GetStockAsync([FromQuery] DateTime start, [FromQuery]  DateTime end)
  {
    var response = await rawMaterialService.GetInventoryAsync(start, end);
    return Ok(response);
  }
}
