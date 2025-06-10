using System;
using System.Collections.Generic;
using DoughManager.Data.BaseEntityModels;

namespace DoughManager.Data.EntityModels;
public class RawMaterialInventory : AuditedEntity
{

    public int RawMaterialId { get; set; }

    public double Quantity { get; set; }

    public virtual RawMaterial RawMaterial { get; set; } = null!;
}
