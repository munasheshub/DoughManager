

using DoughManager.Services.Shared;
using System;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable
namespace DoughManager.Services.Dtos.Production;

public class ProductOrdersDto : BaseEntityDto
{
  public int ProductId { get; set; }

  public int OrderId { get; set; }

  public int? ProductionBatchId { get; set; }

  public bool IsInProduction { get; set; }

  public bool IsFinished { get; set; }
  public string ImageUrl { get; set; }
  public string ProductName { get; set; }

  [Column(TypeName = "decimal(10,2)")]
  public decimal Quantity { get; set; }
}
