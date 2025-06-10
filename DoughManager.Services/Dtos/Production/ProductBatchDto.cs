using System.Text.Json.Serialization;
using DoughManager.Data.Shared;
using DoughManager.Services.Shared;

namespace DoughManager.Services.Dtos.Production;

public class ProductBatchDto : BaseEntityDto
{
    public int ProductionBatchId { get; set; }

    public int ProductId { get; set; }

    public string ProductName { get; set; }

    public string ImageUrl { get; set; }

    public decimal DamagedQuantity { get; set; }

    public decimal QuantityProduced { get; set; }

    public decimal QuantityToBeProduced { get; set; }
    public DateTime StartTime { get; set; }

    public DateTime? EndTime { get; set; }
    public double OpeningZesaUnits { get; set; }


    public double ClosingZesaUnits { get; set; }

    public double ClosingGas { get; set; }

    public double OpeningGas { get; set; }


    [JsonConverter(typeof(JsonStringEnumConverter))]
    public ProductionStatus Status { get; set; }
    public List<ProductionProcessDto>? ProductionProcesses { get; set; }
    public ICollection<ProductionBatchRawMaterialDto>? RawMaterialsUsed { get; set; }

    public int OvenTime { get; set; }
}