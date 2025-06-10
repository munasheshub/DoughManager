using DoughManager.Data.EntityModels;
using DoughManager.Services.Dtos.StockTake;
using DoughManager.Services.Services;
using DoughManager.Services.Shared;

namespace DoughManager.Services.Interfaces;

public interface IStockTakeService
{
    Task<ServiceResponse<StockTakeDto>> RecordTwoPmStockTakeAsync(int productId, double stockLevel);
    Task<ServiceResponse<StockTakeDto>> RecordNinePmStockTakeAsync(int productId, double stockLevel);
    Task<ServiceResponse<StockTakeDto>> GetTodaysStockTakeAsync(int productId);
    Task<ServiceResponse<List<StockTakeDto>>> GetTodayStockTakeAsync();
    Task<ServiceResponse<StockTakeStatus>> CheckStockTakeStatus();
    Task<ServiceResponse<bool>> CloseStockTake(StockTakee request,  bool open);
}