using DoughManager.Data.BaseEntityModels;

namespace DoughManager.Data.EntityModels;

public class StockTake : BaseEntity<int>
{
    public int ProductId { get; set; }
    public DateTime StockTakeDate { get; set; }
    public double? StockLevelAt2PM { get; set; } 
    public double? StockLevelAt9PM { get; set; } 
    public double OpeningStock { get; set; } 
    public double ClosingStock { get; set; } 
    public Product Product { get; set; }

}

public class StockTakeStatus : BaseEntity<int>
{
    public bool IsMorningShiftOpen { get; set; }
    public bool IsMorningShiftClosed { get; set; }
    public bool IsAfternoonShiftOpen { get; set; }
    public bool IsAfternoonShiftClosed { get; set; }
    public TimeSpan MorningShiftOpenTime { get; set; }
    public TimeSpan AfternoonShiftOpenTime { get; set; }
    public TimeSpan AfternoonShiftClosedTime { get; set; }
    public TimeSpan MorningShiftClosedTime { get; set; }
    public DateTime StockTakeDate { get; set; }

}