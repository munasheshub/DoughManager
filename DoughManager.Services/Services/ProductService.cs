// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Services.ProductService
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

#nullable enable
namespace DoughManager.Services.Services;

public class ProductService(
  DoughManagerDbContext context,
  ILogger<ProductService> logger,
  IMapper mapper) : IProductService
{
  public async Task<ServiceResponse<CreateProductRequest>> CreateProductAsync(
    CreateProductRequest request)
  {
    try
    {
      EntityEntry<DoughManager.Data.EntityModels.Product> product = await context.Products.AddAsync(mapper.Map<DoughManager.Data.EntityModels.Product>( request));
      int num = await context.SaveChangesAsync();
      return ServiceResponse<CreateProductRequest>.Success(mapper.Map<CreateProductRequest>((object) await context.Products.Include<DoughManager.Data.EntityModels.Product, ICollection<ProductRawMaterial>>((Expression<Func<DoughManager.Data.EntityModels.Product, ICollection<ProductRawMaterial>>>) (p => p.ProductRawMaterials)).FirstOrDefaultAsync<DoughManager.Data.EntityModels.Product>((Expression<Func<DoughManager.Data.EntityModels.Product, bool>>) (p => p.Id == product.Entity.Id))), "Product created successfully");
    }
    catch (Exception ex)
    {
      logger.LogError("Error creating product: " + ex.Message);
      return ServiceResponse<CreateProductRequest>.Failure(ex.Message);
    }
  }

  public async Task<ServiceResponse<CreateProductRequest>> UpdateProductAsync(
    CreateProductRequest request)
  {
    try
    {
      DoughManager.Data.EntityModels.Product destination = await context.Products.Include<DoughManager.Data.EntityModels.Product, ICollection<ProductRawMaterial>>((Expression<Func<DoughManager.Data.EntityModels.Product, ICollection<ProductRawMaterial>>>) (p => p.ProductRawMaterials)).AsNoTracking<DoughManager.Data.EntityModels.Product>().FirstOrDefaultAsync<DoughManager.Data.EntityModels.Product>((Expression<Func<DoughManager.Data.EntityModels.Product, bool>>) (p => p.Id == request.Id));
      if (destination == null)
        return ServiceResponse<CreateProductRequest>.Failure("Product not found");
      context.Products.Update(mapper.Map<CreateProductRequest, DoughManager.Data.EntityModels.Product>(request, destination));
      int num = await context.SaveChangesAsync();
      return ServiceResponse<CreateProductRequest>.Success(mapper.Map<CreateProductRequest>((object) await context.Products.Include<DoughManager.Data.EntityModels.Product, ICollection<ProductRawMaterial>>((Expression<Func<DoughManager.Data.EntityModels.Product, ICollection<ProductRawMaterial>>>) (p => p.ProductRawMaterials)).FirstOrDefaultAsync<DoughManager.Data.EntityModels.Product>((Expression<Func<DoughManager.Data.EntityModels.Product, bool>>) (p => p.Id == request.Id))), "Product updated successfully");
    }
    catch (Exception ex)
    {
      logger.LogError("Error updating product: " + ex.Message);
      return ServiceResponse<CreateProductRequest>.Failure(ex.Message);
    }
  }

  public async Task<ServiceResponse<List<CreateProductRequest>>> GetAllProductAsync()
  {
    try
    {
      List<DoughManager.Data.EntityModels.Product> listAsync = await context.Products.Include<DoughManager.Data.EntityModels.Product, ICollection<ProductRawMaterial>>((Expression<Func<DoughManager.Data.EntityModels.Product, ICollection<ProductRawMaterial>>>) (p => p.ProductRawMaterials)).AsNoTracking<DoughManager.Data.EntityModels.Product>().ToListAsync<DoughManager.Data.EntityModels.Product>();
      return listAsync == null || !listAsync.Any() ? ServiceResponse<List<CreateProductRequest>>.Failure("No products found") : ServiceResponse<List<CreateProductRequest>>.Success(mapper.Map<List<CreateProductRequest>>( listAsync), "Products retrieved successfully");
    }
    catch (Exception ex)
    {
      logger.LogError("Error retrieving products: " + ex.Message);
      return ServiceResponse<List<CreateProductRequest>>.Failure(ex.Message);
    }
  }

  public async Task<ServiceResponse<List<CreateProductRequest>>> GetAllAvailableProductAsync()
  {
    try
    {
      List<DoughManager.Data.EntityModels.Product> listAsync = await context.Products.Include<DoughManager.Data.EntityModels.Product, ICollection<ProductRawMaterial>>((Expression<Func<DoughManager.Data.EntityModels.Product, ICollection<ProductRawMaterial>>>) (p => p.ProductRawMaterials)).AsNoTracking<DoughManager.Data.EntityModels.Product>().Where<DoughManager.Data.EntityModels.Product>((Expression<Func<DoughManager.Data.EntityModels.Product, bool>>) (p => p.QuantityOnHand > 0.0)).ToListAsync<DoughManager.Data.EntityModels.Product>();
      return listAsync == null || !listAsync.Any() ? ServiceResponse<List<CreateProductRequest>>.Failure("No products found") : ServiceResponse<List<CreateProductRequest>>.Success(mapper.Map<List<CreateProductRequest>>( listAsync), "Products retrieved successfully");
    }
    catch (Exception ex)
    {
      logger.LogError("Error retrieving products: " + ex.Message);
      return ServiceResponse<List<CreateProductRequest>>.Failure(ex.Message);
    }
  }
}
