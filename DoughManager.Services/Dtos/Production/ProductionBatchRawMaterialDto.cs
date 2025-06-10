// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Dtos.Production.ProductionBatchRawMaterialDto
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Services.Shared;
using System;
using System.Text.Json.Serialization;
using DoughManager.Data.Shared;

#nullable disable
namespace DoughManager.Services.Dtos.Production;

public class ProductionBatchRawMaterialDto : BaseEntityDto
{
  public int ProductionBatchId { get; set; }

  public int RawMaterialId { get; set; }
  public string RawMaterialName { get; set; }
  public string ImageUrl { get; set; }
  public int ProductId { get; set; }

  public decimal QuantityUsed { get; set; }

  public bool IsDeleted { get; set; }
  [JsonConverter(typeof(JsonStringEnumConverter))]
  public UnitOfMeasure UnitOfMeasure { get; set; }
}
