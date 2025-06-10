using DoughManager.Services.Dtos.Product;
using DoughManager.Services.Dtos.Production;

namespace DoughManager.Services.Dtos.Dashboard;

public class DashboardDto
{
    public int TotalProducts { get; set; }
    public int TotalActiveProductionBatches { get; set; }
    public int TotalPendingOrders { get; set; }
    public int TotalLowStockItems { get; set; }
    public List<ProductBatchDto> ProductBatches { get; set; }
    public List<OrderDto> Orders { get; set; }
    public List<CreateRawMaterialRequest> LowStockItems { get; set; }
}