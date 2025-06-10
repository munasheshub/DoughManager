// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Dtos.AuthenticationResponse
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

#nullable enable
using DoughManager.Services.Dtos.Auth;

namespace DoughManager.Services.Dtos.Auth;

public class AuthenticationResponse
{
  public string AccessToken { get; set; }

  public string FullName { get; set; }
}
