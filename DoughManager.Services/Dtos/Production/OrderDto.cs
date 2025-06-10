// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Dtos.Production.OrderDto
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

public class OrderDto : FullyAuditedEntityDto
{
  public int Quantity { get; set; }
  public decimal TotalPrice { get; set; }

  public bool IsDispatched { get; set; }

  public bool IsInProduction { get; set; }

  public string CustomerName { get; set; }

  public bool OnHold { get; set; }

  public string? Notes { get; set; }

  public DateTime OrderDate { get; set; }

  public DateTime? DispatchedDate { get; set; }

  [JsonConverter(typeof (JsonStringEnumConverter))]
  public OrderLocation Location { get; set; }

  public List<ProductOrdersDto>? ProductOrders { get; set; }
}
