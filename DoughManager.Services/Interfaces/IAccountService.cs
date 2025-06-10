// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Interfaces.IAccountService
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Services.Dtos.Auth;
using DoughManager.Services.Shared;
using DoughManager.Data.EntityModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using DoughManager.Data.Shared;

#nullable enable
namespace DoughManager.Services.Interfaces;

public interface IAccountService
{
  Task<ServiceResponse<bool>> CreateAccountAsync(CreateAccountRequest request);

  Task<ServiceResponse<AuthenticationResponse>> LoginAsync(AuthenticationRequest request);

  Task<ServiceResponse<AccountDto>> GetAccountAsync(Account account);

  Task<ServiceResponse<List<AccountDto>>> GetAllAccountsAsync();
  
  Task<ServiceResponse<bool>> AdminSetUserActiveStatus(int id, bool active);
  Task<ServiceResponse<bool>> AdminSetUserRole(int id, Role active);
  Task<ServiceResponse<bool>> DeleteAccountAsync(int id);
  Task<ServiceResponse<AccountDto>> UpdateAccountAsync(AccountDto request, Account account);
  Task<ServiceResponse<bool>> SendResetPasswordCode(string email);
  Task<ServiceResponse<bool>> VerifyResetCode(string code, string email);
  Task<ServiceResponse<bool>> ResetPassword(string password, string email);
}
