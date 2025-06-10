using DoughManager.Data.BaseEntityModels;
using DoughManager.Data.Migrations;
using DoughManager.Data.Shared;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DoughManager.Data.EntityModels;

public class ProductionBatch : FullyAuditedEntity
{

    public int QuantityToBeProduced { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }

    public string Sector { get; set; } = null!;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProductionStatus Status { get; set; }
    public string? Notes { get; set; }

    public string? ProductionProcesses { get; set; }

    public double OpeningZesaUnits { get; set; }


    public double ClosingZesaUnits { get; set; }

    public double ClosingGas { get; set; }

    public EnergyUnit EnergyUnit { get; set; }

    public double OpeningGas { get; set; }

    public virtual ICollection<DamagedLog> DamagedLogs { get; set; } = new List<DamagedLog>();

    public virtual ICollection<ProductBatch> ProductBatches { get; set; } = new List<ProductBatch>();

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

    
}
