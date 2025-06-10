// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Dtos.DispatchDto
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Services.Shared;
using System.Collections.Generic;

#nullable enable
namespace DoughManager.Services.Dtos;

public class DispatchDto : FullyAuditedEntityDto
{
  public string DispatchedBy { get; set; }

  public bool IsReceived { get; set; }
  public int OrderId { get; set; }
  public string? InvoiceNumber { get; set; }

  public ICollection<DispatchItemDto>? DispatchItems { get; set; }
}
