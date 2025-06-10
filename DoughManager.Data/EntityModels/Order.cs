using System;
using System.Collections.Generic;
using DoughManager.Data.BaseEntityModels;
using DoughManager.Data.EntityModels;
using DoughManager.Data.Shared;

namespace DoughManager.Data.EntityModels;

public class Order : FullyAuditedEntity
{
    public int Quantity { get; set; }
    public decimal TotalPrice { get; set; }
    public bool IsDispatched { get; set; }

    public string CustomerName { get; set; } = null!;
    public bool IsInProduction { get; set; }
    public bool OnHold { get; set; }

    public string? Notes { get; set; }

    public DateTime OrderDate { get; set; }

    public DateTime? DispatchedDate { get; set; }
    public OrderLocation Location { get; set; }
    public virtual ICollection<Dispatch> Dispatches { get; set; } = new List<Dispatch>();
    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();
}
