using System;
using System.Collections.Generic;
using DoughManager.Data.BaseEntityModels;
using DoughManager.Data.Migrations;

namespace DoughManager.Data.EntityModels;

public class ProductRawMaterial : AuditedEntity
{
    public int Id { get; set; }

    public int ProductId { get; set; }

    public int RawMaterialId { get; set; }

    public decimal QuantityRequired { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual RawMaterial RawMaterial { get; set; } = null!;
}
