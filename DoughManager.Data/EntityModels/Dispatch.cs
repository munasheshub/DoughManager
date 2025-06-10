using System;
using System.Collections.Generic;
using DoughManager.Data.BaseEntityModels;
using DoughManager.Data.Migrations;

namespace DoughManager.Data.EntityModels;

public class Dispatch : FullyAuditedEntity
{
    public string DispatchedBy { get; set; } = null!;

    public bool IsReceived { get; set; }
    public int? OrderId { get; set; }
    
    public string? InvoiceNumber { get; set; }
    public virtual ICollection<DispatchItem> DispatchItems { get; set; } = new List<DispatchItem>();


    public virtual Order? Order { get; set; }

}
