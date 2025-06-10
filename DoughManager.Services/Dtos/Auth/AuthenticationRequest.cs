// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Dtos.AuthenticationRequest
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using System.ComponentModel.DataAnnotations;

#nullable enable
namespace DoughManager.Services.Dtos.Auth;

public class AuthenticationRequest
{
  [Required]
  public string Username { get; set; }

  [Required]
  public string Password { get; set; }
}
