// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Dtos.Production.UpdateProductionBatchStatus
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Data.Shared;
using System;
using System.Text.Json.Serialization;

#nullable disable
namespace DoughManager.Services.Dtos.Production;

public class UpdateProductionBatchStatus
{
  public int BatchId { get; set; }

  [JsonConverter(typeof (JsonStringEnumConverter))]
  public ProductionStatus Status { get; set; }

  public DateTime TimeStamp { get; set; }
}
