// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Shared.ServiceResponse`1
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using System;

#nullable enable
namespace DoughManager.Services.Shared;

public class ServiceResponse<T>
{
  public T? Data { get; set; }

  public string Message { get; set; } = string.Empty;

  public bool IsSuccess { get; set; }

  public DateTime? TimeStamp { get; set; }

  public static ServiceResponse<T> Success(T data, string message = "Success")
  {
    return new ServiceResponse<T>()
    {
      TimeStamp = new DateTime?(DateTime.Now),
      Data = data,
      IsSuccess = true,
      Message = message
    };
  }

  public static ServiceResponse<T> Failure(string message = "Failure")
  {
    return new ServiceResponse<T>()
    {
      TimeStamp = new DateTime?(DateTime.Now),
      Data = default,
      IsSuccess = false,
      Message = message
    };
  }
}
