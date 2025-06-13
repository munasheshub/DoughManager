// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Dtos.Auth.AccountDto
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Services.Shared;
using System.Text.Json.Serialization;
using DoughManager.Data.Shared;

#nullable enable
namespace DoughManager.Services.Dtos.Auth;

public class AccountDto : BaseEntityDto
{
  public string FullName { get; set; }
  public string Email { get; set; }
  public string? ProfileImageUrl { get; set; }

  public string UserName { get; set; }

  [JsonConverter(typeof (JsonStringEnumConverter))]
  public Role Role { get; set; } = Role.Cashier;

  public bool IsActive { get; set; }
  public string? Code { get; set; }
}
