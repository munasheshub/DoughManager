using System;
using System.Collections.Generic;
using DoughManager.Data.BaseEntityModels;
using DoughManager.Data.Migrations;

namespace DoughManager.Data.EntityModels;

public class ProductOrder : BaseEntity<int>
{

    public int ProductId { get; set; }

    public bool IsInProduction { get; set; }

    public bool IsFinished { get; set; }

    public int OrderId { get; set; }

    public decimal Quantity { get; set; }

    public int? ProductionBatchId { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual ProductionBatch? ProductionBatch { get; set; }
}
