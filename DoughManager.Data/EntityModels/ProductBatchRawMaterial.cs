using System;
using System.Collections.Generic;
using DoughManager.Data.BaseEntityModels;
using DoughManager.Data.Migrations;
using DoughManager.Data.Shared;

namespace DoughManager.Data.EntityModels;

public class ProductBatchRawMaterial : BaseEntity<int>
{

    public int ProductBatchId { get; set; }

    public int RawMaterialId { get; set; }

    public decimal QuantityUsed { get; set; }

    public bool IsDeleted { get; set; }

    public int ProductId { get; set; }

    public UnitOfMeasure UnitOfMeasure { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ProductBatch ProductBatch { get; set; } = null!;

    public virtual RawMaterial RawMaterial { get; set; } = null!;
}
