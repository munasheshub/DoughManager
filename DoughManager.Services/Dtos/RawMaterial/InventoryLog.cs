using System.Text.Json.Serialization;
using DoughManager.Data.Shared;

namespace DoughManager.Services.Dtos.RawMaterial;


public class InventoryLog
{
    public int RawMaterialId { get; set; }
    public string RawMaterialName { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public UnitOfMeasure UnitOfMeasure { get; set; }
    public string? ImageUrl { get; set; }
    public string AddedBy { get; set; }
    public DateTime AddedDate { get; set; }
    public int Quantity { get; set; }
    public int Id { get; set; }
}
