// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Interfaces.IJwtUtils
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Data.EntityModels;
using System.Threading.Tasks;

#nullable enable
namespace DoughManager.Services.Interfaces;

public interface IJwtUtils
{
  string GenerateJwtToken(Account account);

  Task<Account?> ValidateJwtToken(string token);
}
