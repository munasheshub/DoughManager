using DoughManager.Data.Shared;
using DoughManager.Services.Dtos.StockTake;
using DoughManager.Services.Interfaces;

namespace DoughManager.Services.Services;

using AutoMapper;
using DoughManager.Services.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DoughManager.Data.DbContexts;
using DoughManager.Data.EntityModels;
using System;
using System.Linq;
using System.Threading.Tasks;

public class StockTakeService : IStockTakeService
{
    private readonly DoughManagerDbContext _context;
    private readonly ILogger<StockTakeService> _logger;
    private readonly IMapper _mapper;
    private readonly IDispatchService _dispatchService;

    public StockTakeService(
        DoughManagerDbContext context,
        ILogger<StockTakeService> logger,
        IMapper mapper)
    {
        _context = context;
        _logger = logger;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<StockTakeDto>> RecordTwoPmStockTakeAsync(int productId, double stockLevel)
    {
        try
        {
            
            var today = DateTime.Today;
            var existingRecord = await _context.StockTakes
                .FirstOrDefaultAsync(s => s.ProductId == productId && s.StockTakeDate == today);

            if (existingRecord == null)
            {
                var openingStock = await GetPreviousDayClosingStockAsync(productId);
                var newRecord = new StockTake
                {
                    ProductId = productId,
                    StockTakeDate = today,
                    OpeningStock = openingStock,
                    StockLevelAt2PM = stockLevel
                };
               _context.StockTakes.Add(newRecord);
            }
            else
            {
                existingRecord.StockLevelAt2PM = stockLevel;
            }

            await _context.SaveChangesAsync();
            
            var stockTake = await _context.StockTakes
                .Include(s => s.Product)
                .FirstOrDefaultAsync(s => s.StockTakeDate.Date == DateTime.Now.Date && s.ProductId == productId);
            var response = _mapper.Map<StockTakeDto>(stockTake);
            return ServiceResponse<StockTakeDto>.Success(response , "2 PM stock recorded successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error recording 2 PM stock: {ex.Message}");
            return ServiceResponse<StockTakeDto>.Failure(ex.Message);
        }
    }

    public async Task<ServiceResponse<StockTakeDto>> RecordNinePmStockTakeAsync(int productId, double stockLevel)
    {
        try
        {
            var today = DateTime.Today;
            var existingRecord = await _context.StockTakes
                .FirstOrDefaultAsync(s => s.ProductId == productId && s.StockTakeDate == today);

            if (existingRecord == null)
            {
                var openingStock = await GetPreviousDayClosingStockAsync(productId);
                var newRecord = new StockTake
                {
                    ProductId = productId,
                    StockTakeDate = today,
                    OpeningStock = openingStock,
                    StockLevelAt2PM = openingStock,
                    StockLevelAt9PM = stockLevel,
                    ClosingStock = stockLevel
                };
                _context.StockTakes.Add(newRecord);
            }
            else
            {
                existingRecord.StockLevelAt9PM = stockLevel;
                existingRecord.ClosingStock = stockLevel;
            }

            await _context.SaveChangesAsync();
            var stockTake = await _context.StockTakes
                .Include(s => s.Product)
                .FirstOrDefaultAsync(s => s.StockTakeDate.Date == DateTime.Now.Date && s.ProductId == productId);
            var response = _mapper.Map<StockTakeDto>(stockTake);
            return ServiceResponse<StockTakeDto>.Success(response , "2 PM stock recorded successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error recording 9 PM stock: {ex.Message}");
            return ServiceResponse<StockTakeDto>.Failure(ex.Message);
        }
    }

    private async Task<double> GetPreviousDayClosingStockAsync(int productId)
    {
        var yesterday = DateTime.Today.AddDays(-1);
        var previousRecord = await _context.StockTakes
            .Where(s => s.ProductId == productId && s.StockTakeDate == yesterday)
            .OrderByDescending(s => s.StockTakeDate)
            .FirstOrDefaultAsync();

        return previousRecord?.ClosingStock ?? await GetInitialProductStockAsync(productId);
    }

    private async Task<double> GetInitialProductStockAsync(int productId)
    {
        var product = await _context.Products.FindAsync(productId);
        return product?.QuantityOnHand ?? 0;
    }

    public async Task<ServiceResponse<StockTakeDto>> GetTodaysStockTakeAsync(int productId)
    {
        try
        {
            var today = DateTime.Today;
            var stockTake = await _context.StockTakes
                .FirstOrDefaultAsync(s => s.ProductId == productId && s.StockTakeDate == today);

            return stockTake == null
                ? ServiceResponse<StockTakeDto>.Failure("No stock take recorded today")
                : ServiceResponse<StockTakeDto>.Success(_mapper.Map<StockTakeDto>(stockTake));
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving stock take: {ex.Message}");
            return ServiceResponse<StockTakeDto>.Failure(ex.Message);
        }
    }

    public async Task<ServiceResponse<List<StockTakeDto>>> GetTodayStockTakeAsync()
    {
        try
        {
            var today = DateTime.Today;
            var stockTake = await _context.StockTakes
                .Include(s => s.Product)
                .Where(s => s.StockTakeDate.Date == today.Date)
                .ToListAsync();
            if (stockTake == null) ServiceResponse<List<StockTakeDto>>.Failure("No stock take recorded today");
            
            var endOfDay = DateTime.Today.AddDays(1);
            var end = endOfDay.AddTicks(-1);
            var morningShiftEndTime = new DateTime(today.Year, today.Month, today.Day, 06, 00, 00);
            var todayDispatches = await _context.DispatchItems
                .Include(di => di.Dispatch)
                .ThenInclude(d => d.Order)
                .Where(di => di.Dispatch.CreationTime >= DateTime.Today.Date && di.Dispatch.CreationTime <= end && di.Dispatch.Order.Location == OrderLocation.External)
                .ToListAsync();
            var totalProduced = await _context.ProductBatches
                .Include(pb => pb.ProductionBatch)
                .Where(di => di.ProductionBatch.CreationTime >= DateTime.Today.Date && di.ProductionBatch.CreationTime <= end)
                .ToListAsync();
            
            var stokeTakeStatus = await _context.StockTakeStatus.FirstOrDefaultAsync(sts => sts.StockTakeDate.Date == today.Date);
            

            var response = _mapper.Map<List<StockTakeDto>>(stockTake);
            foreach (var item in response)
            {
                item.ExternalDispatch2PM = todayDispatches.FirstOrDefault(di => di.ProductId == item.ProductId && di.Dispatch.CreationTime.Value.TimeOfDay <= stokeTakeStatus.MorningShiftClosedTime)?.Quantity ?? 0.0;
                item.Produced2PM = totalProduced
                    .FirstOrDefault(p => p.ProductId == item.ProductId 
                                         && (p.ProductionBatch.CreationTime >= morningShiftEndTime 
                                             && p.ProductionBatch.CreationTime.Value.TimeOfDay <= stokeTakeStatus.MorningShiftClosedTime))?.QuantityProduced ?? 0.0;
                item.ExternalDispatch9PM = todayDispatches.FirstOrDefault(di => di.ProductId == item.ProductId && di.Dispatch.CreationTime.Value.TimeOfDay <= stokeTakeStatus.AfternoonShiftClosedTime)?.Quantity ?? 0.0;
                item.Produced9PM = totalProduced
                    .FirstOrDefault(p => p.ProductId == item.ProductId 
                                         && (p.ProductionBatch.CreationTime.Value.TimeOfDay >= stokeTakeStatus.MorningShiftClosedTime 
                                             && p.ProductionBatch.CreationTime.Value.TimeOfDay <= stokeTakeStatus.AfternoonShiftClosedTime))?.QuantityProduced ?? 0.0;
                
                if (stokeTakeStatus.IsMorningShiftClosed && stokeTakeStatus.IsAfternoonShiftClosed)
                {
                    item.SoldStock2PM = (item.OpeningStock + item.Produced2PM) - (item.StockLevelAt2PM + item.ExternalDispatch2PM);
                    item.SoldStock9PM = (item.StockLevelAt2PM + item.Produced9PM) - (item.StockLevelAt9PM + item.ExternalDispatch9PM);
                }
                else if(stokeTakeStatus.IsMorningShiftClosed)
                {
                    item.SoldStock2PM = (item.OpeningStock + item.Produced2PM) - (item.StockLevelAt2PM + item.ExternalDispatch2PM);
                    item.SoldStock9PM = 0.0;
                }
                else
                {
                    item.SoldStock2PM = 0.0;
                    item.SoldStock9PM = 0.0;
                }
                
            }

            return ServiceResponse<List<StockTakeDto>>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error retrieving stock take: {ex.Message}");
            return ServiceResponse<List<StockTakeDto>>.Failure(ex.Message);
        }
    }

    public async Task<ServiceResponse<StockTakeStatus>> CheckStockTakeStatus()
    {
        try
        {
            var today = DateTime.Today;
            var status = await _context.StockTakeStatus.FirstOrDefaultAsync(sts => sts.StockTakeDate.Date == today.Date);
            if(status == null) return ServiceResponse<StockTakeStatus>.Failure(null);
            return ServiceResponse<StockTakeStatus>.Success(status);
            
            
        }
        catch (Exception e)
        {
            return ServiceResponse<StockTakeStatus>.Failure("Failed to check status: " + e.Message);
        }
    }

    public async Task<ServiceResponse<bool>> CloseStockTake(StockTakee request, bool open)
    {
        try
        {
            if (open)
            {
                if (request == StockTakee.Morning)
                {
                    
                    var newRecord = new StockTakeStatus
                    {
                        StockTakeDate = DateTime.Now,
                        IsAfternoonShiftOpen = request == StockTakee.Afternoon,
                        IsMorningShiftOpen = request == StockTakee.Morning,
                        MorningShiftOpenTime = DateTime.Now.TimeOfDay,
                        IsAfternoonShiftClosed = false,
                        IsMorningShiftClosed = false

                    };
                    await _context.StockTakeStatus.AddAsync(newRecord);
                }
                else
                {
                    var stockTake = await
                        _context.StockTakeStatus.FirstOrDefaultAsync(sts => sts.StockTakeDate.Date == DateTime.Now.Date);
                    stockTake.IsAfternoonShiftOpen = true;
                    stockTake.IsMorningShiftOpen = false;
                    stockTake.AfternoonShiftOpenTime = DateTime.Now.TimeOfDay;
                    _context.StockTakeStatus.Update(stockTake);
                }

            }else
            {
                var stockTake = await
                    _context.StockTakeStatus.FirstOrDefaultAsync(sts => sts.StockTakeDate.Date == DateTime.Now.Date);
                if (request == StockTakee.Afternoon)
                {
                    stockTake.IsAfternoonShiftClosed = true;
                    stockTake.IsAfternoonShiftOpen = false;
                    stockTake.AfternoonShiftClosedTime = DateTime.Now.TimeOfDay;
                
                }
                else
                {
                    stockTake.IsMorningShiftClosed = true;
                    stockTake.IsMorningShiftOpen = false;
                    stockTake.MorningShiftClosedTime = DateTime.Now.TimeOfDay;
                }
                _context.StockTakeStatus.Update(stockTake);
                
            }
            await _context.SaveChangesAsync(); 
            return ServiceResponse<bool>.Success(true);
        }
        catch (Exception e)
        {
            return ServiceResponse<bool>.Failure("Failed to check status: " + e.Message);
        }
    }
}

public enum StockTakee{ Morning, Afternoon}
