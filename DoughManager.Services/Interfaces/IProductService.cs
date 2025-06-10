// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Interfaces.IProductService
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Services.Dtos.Product;
using DoughManager.Services.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable
namespace DoughManager.Services.Interfaces;

public interface IProductService
{
  Task<ServiceResponse<CreateProductRequest>> CreateProductAsync(CreateProductRequest request);

  Task<ServiceResponse<CreateProductRequest>> UpdateProductAsync(CreateProductRequest request);

  Task<ServiceResponse<List<CreateProductRequest>>> GetAllProductAsync();

  Task<ServiceResponse<List<CreateProductRequest>>> GetAllAvailableProductAsync();
}
