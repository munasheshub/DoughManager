// Decompiled with JetBrains decompiler
// Type: MoringaBakery.Api.Controllers.ProductionBatchController
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
public class ProductionBatchController(IProductionBatchService productionBatchService) : 
  BaseController
{
  [HttpPost("create")]
  public async Task<ActionResult<ServiceResponse<ProductionBatchDto>>> CreateAsync(
    ProductionBatchDto request)
  {
    ProductionBatchController productionBatchController = this;
    ServiceResponse<ProductionBatchDto> async = await productionBatchService.CreateAsync(request, productionBatchController.Account);
    return (ActionResult<ServiceResponse<ProductionBatchDto>>) (ActionResult) productionBatchController.Ok((object) async);
  }

  [HttpPut("updateStatus")]
  public async Task<ActionResult<ServiceResponse<ProductionBatchDto>>> UpdateStatusAsync(
    UpdateProductionBatchStatus request)
  {
    ProductionBatchController productionBatchController = this;
    ServiceResponse<ProductionBatchDto> serviceResponse = await productionBatchService.UpdateStatusAsync(request, productionBatchController.Account);
    return (ActionResult<ServiceResponse<ProductionBatchDto>>) (ActionResult) productionBatchController.Ok((object) serviceResponse);
  }
  
  [HttpPut("updateProductStatus")]
  public async Task<ActionResult<ServiceResponse<ProductBatchDto>>> UpdateProductStatusAsync(
    ProductBatchDto request)
  {
    var response = await productionBatchService.UpdateProductStatusAsync(request);
    return Ok(response);
  }
  
  [HttpPut("addProduct")]
  public async Task<ActionResult<ServiceResponse<ProductBatchDto>>> UpdateProductAsync(
    ProductBatchDto request)
  {
    var response = await productionBatchService.AddProductAsync(request, Account);
    return Ok(response);
  }

  [HttpGet("updateQuantity")]
  public async Task<ActionResult<ServiceResponse<ProductionBatchDto>>> UpdateQuantityAsync(
    [FromQuery] int quantity,
    [FromQuery] int batchId)
  {
    ProductionBatchController productionBatchController = this;
    ServiceResponse<ProductionBatchDto> serviceResponse = await productionBatchService.UpdateQuantityAsync(quantity, batchId, productionBatchController.Account);
    return (ActionResult<ServiceResponse<ProductionBatchDto>>) (ActionResult) productionBatchController.Ok((object) serviceResponse);
  }

  [HttpGet("getAll")]
  public async Task<ActionResult<ServiceResponse<List<ProductionBatchDto>>>> GetAllAsync([FromQuery] DateTime start, [FromQuery] DateTime end)
  {
    ProductionBatchController productionBatchController = this;
    ServiceResponse<List<ProductionBatchDto>> allAsync = await productionBatchService.GetAllAsync(start, end);
    return (ActionResult<ServiceResponse<List<ProductionBatchDto>>>) (ActionResult) productionBatchController.Ok((object) allAsync);
  }
}
