using System;
using System.Collections.Generic;
using DoughManager.Data.BaseEntityModels;
using DoughManager.Data.Migrations;

namespace DoughManager.Data.EntityModels;

public partial class DispatchItem : BaseEntity<int>
{

    public int DispatchId { get; set; }

    public int ProductId { get; set; }

    public double Quantity { get; set; }

    public string? ReceivedBy { get; set; }

    public string? ReceivedNotes { get; set; }
    public DateTime? ReceivedDate { get; set; }

    public double ReceivedQuantity { get; set; }

    public virtual Dispatch Dispatch { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

}
