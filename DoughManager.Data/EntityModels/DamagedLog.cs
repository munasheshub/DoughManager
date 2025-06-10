using System;
using System.Collections.Generic;
using DoughManager.Data.BaseEntityModels;
using DoughManager.Data.Migrations;

namespace DoughManager.Data.EntityModels;

public partial class DamagedLog : FullyAuditedEntity
{

    public int ProductionBatchId { get; set; }

    public decimal DamagedQuantity { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ProductionBatch ProductionBatch { get; set; } = null!;
}
