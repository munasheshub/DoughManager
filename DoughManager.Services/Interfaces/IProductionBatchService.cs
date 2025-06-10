// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Interfaces.IProductionBatchService
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Services.Dtos.Production;
using DoughManager.Services.Shared;
using DoughManager.Data.EntityModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoughManager.Data.Shared;

#nullable enable
namespace DoughManager.Services.Interfaces;

public interface IProductionBatchService
{
  Task<ServiceResponse<ProductionBatchDto>> CreateAsync(ProductionBatchDto request, Account account);
  Task<ServiceResponse<ProductionBatchDto>> AddProductAsync(ProductBatchDto request, Account account);

  Task<ServiceResponse<List<ProductionBatchDto>>> GetAllAsync(DateTime start, DateTime end);

  Task<ServiceResponse<ProductionBatchDto>> UpdateStatusAsync(
    UpdateProductionBatchStatus newStatus,
    Account account);
  
  Task<ServiceResponse<ProductBatchDto>> UpdateProductStatusAsync(
    ProductBatchDto productStatus);

  Task<ServiceResponse<ProductionBatchDto>> UpdateQuantityAsync(
    int quantity,
    int batchId,
    Account account);
}

public class ProductStatus
{
  public int ProductionBatchId { get; set; }
  public ProductionStatus ProductionStatus { get; set; }
  public int ProductId { get; set; }
}