// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Shared.FullyAuditedEntityDto
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Data.BaseEntityModels;
using System;

#nullable disable
namespace DoughManager.Services.Shared;

public class FullyAuditedEntityDto : AuditedEntityDto
{
  public int? LastModifierId { get; set; }

  public DateTime? LastModificationTime { get; set; }
}
