using System;
using System.Collections.Generic;
using DoughManager.Data.BaseEntityModels;

namespace DoughManager.Data.EntityModels;

public class RawMaterial : AuditedEntity
{

    public string Name { get; set; } = null!;

    public int UnitOfMeasure { get; set; }

    public decimal QuantityOnHand { get; set; }

    public decimal DamagedQuantity { get; set; }

    public decimal IdealQuantity { get; set; }

    public string? ImageUrl { get; set; }

    public decimal CostPrice { get; set; }

    public virtual ICollection<ProductRawMaterial> ProductRawMaterials { get; set; } = new List<ProductRawMaterial>();

    public virtual ICollection<ProductBatchRawMaterial> ProductBatchRawMaterials { get; set; } = new List<ProductBatchRawMaterial>();

    public virtual ICollection<RawMaterialInventory> RawMaterialInventories { get; set; } = new List<RawMaterialInventory>();

    public virtual ICollection<ReceivingLog> ReceivingLogs { get; set; } = new List<ReceivingLog>();
}
