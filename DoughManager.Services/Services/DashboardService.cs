using AutoMapper;
using DoughManager.Data.DbContexts;
using DoughManager.Data.Shared;
using DoughManager.Services.Dtos.Dashboard;
using DoughManager.Services.Dtos.Product;
using DoughManager.Services.Dtos.Production;
using DoughManager.Services.Interfaces;
using DoughManager.Services.Shared;
using Microsoft.EntityFrameworkCore;

namespace DoughManager.Services.Services;

public class DashboardService : IDashboardService
{
    private readonly DoughManagerDbContext _context;
    private readonly IMapper _mapper;

    public DashboardService(DoughManagerDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<DashboardDto>> GetDashboardAsync()
    {
        try
        {
            var totalProducts = await _context.Products.Where(p => !p.IsDeleted).CountAsync();
            var totalPendingOrders = await _context.Orders
                .Include(o => o.ProductOrders)
                .ThenInclude(po => po.Product)
                .Where(o => !o.IsDispatched)
                .OrderByDescending(o => o.CreationTime)
                .Take(3)
                .ToListAsync();
            var totalPendingBatchs = await _context.ProductBatches 
                .Include(pb => pb.Product)
                .Include(pb => pb.ProductionBatch)
                .Where(pb => pb.ProductionBatch.Status == ProductionStatus.Inprogress || pb.ProductionBatch.Status == ProductionStatus.Mixing)
                .OrderByDescending(o => o.StartTime)
                .Take(3)
                .ToListAsync();
            var totalLowStockItems = await _context.RawMaterials
                .Where(rm => rm.QuantityOnHand < rm.IdealQuantity)
                .ToListAsync();

            var dashboardDto = new DashboardDto
            {
                LowStockItems = _mapper.Map<List<CreateRawMaterialRequest>>(totalLowStockItems),
                ProductBatches = _mapper.Map<List<ProductBatchDto>>(totalPendingBatchs),
                Orders = _mapper.Map<List<OrderDto>>(totalPendingOrders),
                TotalActiveProductionBatches = totalPendingBatchs.Count,
                TotalLowStockItems = totalLowStockItems.Count,
                TotalPendingOrders = totalPendingOrders.Count,
                TotalProducts = totalProducts
            };
            
            return ServiceResponse<DashboardDto>.Success(dashboardDto);

        }
        catch (Exception e)
        {
            return ServiceResponse<DashboardDto>.Failure($"Failed to load dashboard:" + e.Message);
        }
    }
}