using DoughManager.Services.Shared;

namespace DoughManager.Services.Dtos.StockTake;
public class StockTakeDto : BaseEntityDto
{
    public int ProductId { get; set; }
    public string ProductName { get; set; }
    public string ImageUrl { get; set; }
    public DateTime StockTakeDate { get; set; }
    public double? StockLevelAt2PM { get; set; } 
    public double? StockLevelAt9PM { get; set; } 
    public double SellingPrice { get; set; } 
    public double OpeningStock { get; set; } 
    public double ClosingStock { get; set; } 
    public double ExternalDispatch2PM { get; set; } 
    public double Produced2PM { get; set; } 
    public double ExternalDispatch9PM { get; set; } 
    public double Produced9PM { get; set; } 
    public double? SoldStock2PM { get; set; } 
    public double? SoldStock9PM { get; set; } 

}