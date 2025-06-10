using System.Text.Json.Serialization;
using DoughManager.Data.EntityModels;
using DoughManager.Services.Shared;

namespace DoughManager.Services.Dtos.Discrepancy;

public class DiscrepancyStatus
{
    public DateTime Date { get; set; }
    public DiscrepancyRecordDto? MorningShiftRecord { get; set; }
    public DiscrepancyRecordDto? AfternoonShiftRecord { get; set; }
    public List<DiscrepancyRecordDto> AllRecords { get; set; }
}

public class DiscrepancyRecordDto : AuditedEntityDto
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public double Amount { get; set; }
    public string Notes { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public DiscrepancyType Type { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ShiftType Shift { get; set; }
    
}