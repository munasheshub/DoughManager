using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using DoughManager.Data.BaseEntityModels;
using DoughManager.Data.Migrations;
using DoughManager.Data.Shared;

namespace DoughManager.Data.EntityModels;

public partial class ProductBatch : BaseEntity<int>
{

    public int ProductionBatchId { get; set; }

    public int ProductId { get; set; }

    public int QuantityProduced { get; set; }

    public string ProductName { get; set; } = null!;

    public string ImageUrl { get; set; } = null!;

    public int OvenTime { get; set; }

    public int DamagedQuantity { get; set; }

    public int QuantityToBeProduced { get; set; }
    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }
    public double OpeningZesaUnits { get; set; }


    public double ClosingZesaUnits { get; set; }

    public double ClosingGas { get; set; }

    public double OpeningGas { get; set; }


    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProductionStatus Status { get; set; }
    public string? ProductionProcesses { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ProductionBatch ProductionBatch { get; set; } = null!;
    public virtual ICollection<ProductBatchRawMaterial> RawMaterialsUsed { get; set; } = new List<ProductBatchRawMaterial>();
}
