// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Interfaces.IDispatchService
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Services.Dtos;
using DoughManager.Services.Shared;
using DoughManager.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable enable
namespace DoughManager.Services.Interfaces;

public interface IDispatchService
{
  Task<ServiceResponse<DispatchDto>> CreateAsync(
    DispatchDto dispatchItem,
    Account account);
  
  Task<ServiceResponse<DispatchDto>> ReceiveAsync(
    DispatchDto dispatchItem,
    Account account);

  Task<ServiceResponse<List<DispatchDto>>> GetDispatchedItemsAsync(
    DateTime startDate,
    DateTime endDate);
  
  Task<ServiceResponse<List<DispatchDto>>> GetReceivedItemsAsync(
    DateTime startDate,
    DateTime endDate);
  
  Task<ServiceResponse<List<DispatchDto>>> PendingReceivalAsync();
  
}
