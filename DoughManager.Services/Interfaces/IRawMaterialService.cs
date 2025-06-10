// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Interfaces.IRawMaterialService
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Services.Dtos.Product;
using DoughManager.Services.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoughManager.Data.EntityModels;
using DoughManager.Services.Dtos.RawMaterial;

#nullable enable
namespace DoughManager.Services.Interfaces;

public interface IRawMaterialService
{
  Task<ServiceResponse<CreateRawMaterialRequest>> CreateRawMaterialAsync(
    CreateRawMaterialRequest request);

  Task<ServiceResponse<CreateRawMaterialRequest>> UpdateRawMaterialAsync(
    CreateRawMaterialRequest request);
  
  Task<ServiceResponse<bool>> AddStockAsync(
    int rawmaterialId, decimal quantity, Account account);
  
  Task<ServiceResponse<List<InventoryLog>>> GetInventoryAsync(
    DateTime start, DateTime end);
  
  Task<ServiceResponse<bool>> DeleteAsync(
    int rawmaterial);

  Task<ServiceResponse<List<CreateRawMaterialRequest>>> GetAllAsync();
}
