using System;
using System.Collections.Generic;
using DoughManager.Data.BaseEntityModels;

namespace DoughManager.Data.EntityModels;

public class ReceivingLog : AuditedEntity
{

    public int RawMaterialId { get; set; }

    public decimal QuantityReceived { get; set; }

    public DateTime ReceivedDate { get; set; }

    public string Condition { get; set; } = null!;

    public string Supplier { get; set; } = null!;


    public virtual RawMaterial RawMaterial { get; set; } = null!;
}
