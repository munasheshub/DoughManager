// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Dtos.Production.ProductionProcessDto
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Data.Shared;
using System;
using System.Text.Json.Serialization;

#nullable enable
namespace DoughManager.Services.Dtos.Production;

public class ProductionProcessDto
{
  [JsonConverter(typeof (JsonStringEnumConverter))]
  public ProductionStatus ProcessName { get; set; }

  public DateTime StartTime { get; set; }

  public DateTime? EndTime { get; set; }

  public string? Notes { get; set; }
}
