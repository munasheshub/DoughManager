// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Mapping.ProductionProcessesResolver
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

public class ProductionProcessesResolver :
  IValueResolver<ProductionBatch, ProductionBatchDto, List<ProductionProcessDto>?>
{
  public List<ProductionProcessDto>? Resolve(ProductionBatch source, ProductionBatchDto destination, List<ProductionProcessDto>? destMember, ResolutionContext context)
  {
    Console.WriteLine("PRODUCTION PROCESS RESOLVER HIT");
    return !string.IsNullOrEmpty(source.ProductionProcesses)
      ? JsonSerializer.Deserialize<List<ProductionProcessDto>>(source.ProductionProcesses)
      : null;
  }
}
public class ProductProcessesResolver :
  IValueResolver<ProductBatch, ProductBatchDto, List<ProductionProcessDto>?>
{
  public List<ProductionProcessDto>? Resolve(ProductBatch source, ProductBatchDto destination, List<ProductionProcessDto>? destMember, ResolutionContext context)
  {
    Console.WriteLine("PRODUCTION PROCESS RESOLVER HIT");
    return !string.IsNullOrEmpty(source.ProductionProcesses)
      ? JsonSerializer.Deserialize<List<ProductionProcessDto>>(source.ProductionProcesses)
      : null;
  }
}