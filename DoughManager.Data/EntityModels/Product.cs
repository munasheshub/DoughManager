using System;
using System.Collections.Generic;
using DoughManager.Data.BaseEntityModels;
using DoughManager.Data.EntityModels;

namespace DoughManager.Data.EntityModels;

public class Product : AuditedEntity
{

    public string Name { get; set; } = null!;

    public int OvenTime { get; set; }

    public double QuantityOnHand { get; set; }

    public decimal IdealQuantity { get; set; }

    public int Calories { get; set; }

    public int Fat { get; set; }

    public int Protein { get; set; }

    public int Carbs { get; set; }

    public bool IsProduced { get; set; }

    public int? CategoryId { get; set; }

    public string? ImageUrl { get; set; }


    public decimal SellingPrice { get; set; }

    public string? ProductRetailerId { get; set; }

    public virtual Category? Category { get; set; }


    public virtual ICollection<DailyInventory> DailyInventories { get; set; } = new List<DailyInventory>();

    public virtual ICollection<DamagedLog> DamagedLogs { get; set; } = new List<DamagedLog>();


    public virtual ICollection<DispatchItem> DispatchItems { get; set; } = new List<DispatchItem>();

    public virtual ICollection<ProductBatch> ProductBatches { get; set; } = new List<ProductBatch>();

    public virtual ICollection<ProductOrder> ProductOrders { get; set; } = new List<ProductOrder>();

    public virtual ICollection<ProductRawMaterial> ProductRawMaterials { get; set; } = new List<ProductRawMaterial>();

    public virtual ICollection<ProductBatchRawMaterial> ProductionBatchRawMaterials { get; set; } = new List<ProductBatchRawMaterial>();

    public virtual ICollection<ProductionBatch> ProductionBatches { get; set; } = new List<ProductionBatch>();
    public List<StockTake> StockTakes { get; set; } = new List<StockTake>();
}
