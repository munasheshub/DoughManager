// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Dtos.Product.CreateProductRawMaterialRequest
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Services.Shared;
using System;

#nullable disable
namespace DoughManager.Services.Dtos.Product;

public class CreateProductRawMaterialRequest : BaseEntityDto
{
  public int ProductId { get; set; }

  public int RawMaterialId { get; set; }

  public decimal QuantityRequired { get; set; }

  public bool IsDeleted { get; set; }
}
