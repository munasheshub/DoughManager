// Decompiled with JetBrains decompiler
// Type: MoringaBakery.Api.Controllers.ProductController
// Assembly: MoringaBakery.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B2D6541-2FB5-408C-932B-3F249436BB09
// Assembly location: C:\Users\munas\OneDrive\Desktop\moringaBakery (2)\moringaBakery\MoringaBakery.Api.dll

#nullable enable
using DoughManager.Services.Dtos.Product;
using DoughManager.Services.Interfaces;
using DoughManager.Services.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DoughManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController(IProductService productService) : ControllerBase
{
  [HttpPost("create")]
  public async Task<ActionResult<ServiceResponse<CreateProductRequest>>> CreateAsync(
    CreateProductRequest request)
  {
    ProductController productController = this;
    ServiceResponse<CreateProductRequest> productAsync = await productService.CreateProductAsync(request);
    return (ActionResult<ServiceResponse<CreateProductRequest>>) (ActionResult) productController.Ok((object) productAsync);
  }

  [HttpPut("update")]
  public async Task<ActionResult<ServiceResponse<CreateProductRequest>>> UpdateAsync(
    CreateProductRequest request)
  {
    ProductController productController = this;
    ServiceResponse<CreateProductRequest> serviceResponse = await productService.UpdateProductAsync(request);
    return (ActionResult<ServiceResponse<CreateProductRequest>>) (ActionResult) productController.Ok((object) serviceResponse);
  }

  [HttpGet("getAll")]
  public async Task<ActionResult<ServiceResponse<List<CreateProductRequest>>>> GetAllAsync()
  {
    ProductController productController = this;
    ServiceResponse<List<CreateProductRequest>> allProductAsync = await productService.GetAllProductAsync();
    return (ActionResult<ServiceResponse<List<CreateProductRequest>>>) (ActionResult) productController.Ok((object) allProductAsync);
  }

  [HttpGet("getAllAvailable")]
  public async Task<ActionResult<ServiceResponse<List<CreateProductRequest>>>> GetAllAvailableAsync()
  {
    ProductController productController = this;
    ServiceResponse<List<CreateProductRequest>> availableProductAsync = await productService.GetAllAvailableProductAsync();
    return (ActionResult<ServiceResponse<List<CreateProductRequest>>>) (ActionResult) productController.Ok((object) availableProductAsync);
  }
}
