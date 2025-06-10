// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Dtos.Production.ProductionBatchDto
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Services.Shared;
using DoughManager.Data.Shared;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable enable
namespace DoughManager.Services.Dtos.Production;

public class ProductionBatchDto : FullyAuditedEntityDto
{
  

  public int QuantityToBeProduced { get; set; }

  public DateTime StartTime { get; set; }

  public DateTime? EndTime { get; set; }


  public string Sector { get; set; }

  [JsonConverter(typeof (JsonStringEnumConverter))]
  public ProductionStatus Status { get; set; }

  public string? Notes { get; set; }
  public decimal OpeningZesaUnits { get; set; }
  public decimal ClosingZesaUnits { get; set; }
  public decimal OpeningGas { get; set; }
  public decimal ClosingGas { get; set; }
  [JsonConverter(typeof (JsonStringEnumConverter))]
  public EnergyUnit EnergyUnit { get; set; }

  public List<ProductBatchDto>? ProductBatches { get; set; }

  public List<ProductionProcessDto>? ProductionProcesses { get; set; }

  public string? ProductName { get; set; }

  public int OvenTime { get; set; }

  public List<int>? Orders { get; set; }

  
}
