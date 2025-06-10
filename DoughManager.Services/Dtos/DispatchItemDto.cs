// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Dtos.DispatchItemDto
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Data.BaseEntityModels;

#nullable disable
namespace DoughManager.Services.Dtos;

public class DispatchItemDto : BaseEntity<int>
{
  public int DispatchId { get; set; }
  public int ProductId { get; set; }
  public double Quantity { get; set; }
  public string ProductName { get; set; }
  public string ImageUrl { get; set; }

  public string ReceivedBy { get; set; }

  public string ReceivedNotes { get; set; }
  public DateTime? ReceivedDate { get; set; }

  public double ReceivedQuantity { get; set; }
}
