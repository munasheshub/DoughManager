// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Dtos.Product.CreateProductRequest
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Services.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable enable
namespace DoughManager.Services.Dtos.Product;

public class CreateProductRequest : BaseEntityDto
{
  [Required]
  [StringLength(255 /*0xFF*/)]
  public string Name { get; set; }

  public int OvenTime { get; set; }

  [Column(TypeName = "decimal(10,2)")]
  public decimal QuantityOnHand { get; set; }

  [Column(TypeName = "decimal(10,2)")]
  public decimal IdealQuantity { get; set; }
  
  [Column(TypeName = "decimal(10,2)")]
  public decimal SellingPrice { get; set; }

  public int Calories { get; set; }

  public int Fat { get; set; }

  public int Protein { get; set; }

  public int Carbs { get; set; }

  public bool IsProduced { get; set; }

  public int? CategoryId { get; set; } = new int?(1);

  public string? ImageUrl { get; set; }

  public ICollection<CreateProductRawMaterialRequest>? ProductRawMaterials { get; set; }
}
