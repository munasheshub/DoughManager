// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Dtos.Product.CreateRawMaterialRequest
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Services.Shared;
using DoughManager.Data.Shared;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

#nullable enable
namespace DoughManager.Services.Dtos.Product;

public class CreateRawMaterialRequest : BaseEntityDto
{
  [Required]
  [StringLength(255 /*0xFF*/)]
  public string Name { get; set; }

  [JsonConverter(typeof (JsonStringEnumConverter))]
  public UnitOfMeasure UnitOfMeasure { get; set; }

  [Column(TypeName = "decimal(10,2)")]
  public decimal QuantityOnHand { get; set; }

  [Column(TypeName = "decimal(10,2)")]
  public decimal DamagedQuantity { get; set; }

  [Column(TypeName = "decimal(10,2)")]
  public decimal IdealQuantity { get; set; }
  
  [Column(TypeName = "decimal(10,2)")]
  public decimal CostPrice { get; set; }

  public string? ImageUrl { get; set; }
}
