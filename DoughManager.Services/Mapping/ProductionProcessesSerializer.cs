// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Mapping.ProductionProcessesSerializer
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using AutoMapper;
using DoughManager.Services.Dtos.Production;
using DoughManager.Data.EntityModels;
using System.Collections.Generic;
using System.Text.Json;

#nullable enable
namespace DoughManager.Services.Mapping;

public class ProductionProcessesSerializer :
  IValueResolver<ProductionBatchDto, ProductionBatch, string?>
{
  public string? Resolve(ProductionBatchDto source, ProductionBatch destination, string? destMember, ResolutionContext context)
  {
    return source.ProductionProcesses != null
      ? JsonSerializer.Serialize(source.ProductionProcesses)
      : null;
  }
}
public class ProductProcessesSerializer :
  IValueResolver<ProductBatchDto, ProductBatch, string?>
{
  public string? Resolve(ProductBatchDto source, ProductBatch destination, string? destMember, ResolutionContext context)
  {
    return source.ProductionProcesses != null
      ? JsonSerializer.Serialize(source.ProductionProcesses)
      : null;
  }
}