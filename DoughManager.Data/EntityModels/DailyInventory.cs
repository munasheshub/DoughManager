using System;
using System.Collections.Generic;
using DoughManager.Data.BaseEntityModels;
using DoughManager.Data.Migrations;

namespace DoughManager.Data.EntityModels;

public partial class DailyInventory : FullyAuditedEntity
{

    public int ProductId { get; set; }

    public DateTime Date { get; set; }

    public double OpeningQuantity { get; set; }

    public double ClosingQuantity { get; set; }

    public double ProducedQuantityMorning { get; set; }

    public string? Notes { get; set; }


    public double ProducedQuantityAfternoon { get; set; }

    public double SoldCountAfternoon { get; set; }

    public double SoldCountMorning { get; set; }

    public double DayEndQuantity { get; set; }

    public double DispatchedQuantityAfternoon { get; set; }

    public double DispatchedQuantityMorning { get; set; }

    public bool IsMorningShiftClosed { get; set; }

    public double PhysicalCountAfternoon { get; set; }

    public double PhysicalCountMorning { get; set; }

    public bool IsAfternoonShiftClosed { get; set; }


    public virtual Product Product { get; set; } = null!;
}
