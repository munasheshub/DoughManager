// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Services.AccountService
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using AutoMapper;
using DoughManager.Services.Dtos.Auth;
using DoughManager.Services.Interfaces;
using DoughManager.Services.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DoughManager.Data.DbContexts;
using DoughManager.Data.EntityModels;
using DoughManager.Data.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

#nullable enable
namespace DoughManager.Services.Services;

public class AccountService(
  DoughManagerDbContext context,
  ILogger<AccountService> logger,
  IMapper mapper,
  IEmailService emailService,
  IJwtUtils jwtUtils) : IAccountService
{
  public async Task<ServiceResponse<bool>> CreateAccountAsync(CreateAccountRequest request)
  {
    try
    {
      if (context.Accounts.Any<Account>((Expression<Func<Account, bool>>) (a => a.UserName == request.UserName)))
        return ServiceResponse<bool>.Failure("Account already exists");
      Account newAccount = mapper.Map<Account>( request);
      newAccount.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newAccount.PasswordHash);
      bool flag = await context.Accounts.AnyAsync<Account>();
      newAccount.Role = flag ? Role.Manager : Role.Cashier;
      newAccount.IsActive = flag;
      context.Accounts.Add(newAccount);
      int num = await context.SaveChangesAsync();
      return ServiceResponse<bool>.Success(true, "Account created successfully");
    }
    catch (Exception ex)
    {
      logger.LogError("Error creating account: " + ex.Message);
      return ServiceResponse<bool>.Failure(ex.Message);
    }
  }

  public async Task<ServiceResponse<AuthenticationResponse>> LoginAsync(
    AuthenticationRequest request)
  {
    try
    {
      Account account = await context.Accounts.FirstOrDefaultAsync(a => a.UserName == request.Username || a.Email == request.Username);
      if (account == null)
        return ServiceResponse<AuthenticationResponse>.Failure("Account not found");
      if (!BCrypt.Net.BCrypt.Verify(request.Password, account.PasswordHash))
        return ServiceResponse<AuthenticationResponse>.Failure("Invalid username/password");
      string jwtToken = jwtUtils.GenerateJwtToken(account);
      return ServiceResponse<AuthenticationResponse>.Success(new AuthenticationResponse()
      {
        AccessToken = jwtToken,
        FullName = account.FullName
      });
    }
    catch (Exception ex)
    {
      logger.LogError("Error creating account: " + ex.Message);
      return ServiceResponse<AuthenticationResponse>.Failure(ex.Message);
    }
  }

  public async Task<ServiceResponse<AccountDto>> GetAccountAsync(Account account)
  {
    try
    {
      return ServiceResponse<AccountDto>.Success(mapper.Map<AccountDto>(account));
    }
    catch (Exception ex)
    {
      logger.LogError("Error creating account: " + ex.Message);
      return ServiceResponse<AccountDto>.Failure(ex.Message);
    }
  }

  public async Task<ServiceResponse<List<AccountDto>>> GetAllAccountsAsync()
  {
    try
    {
      var accounts = await context.Accounts.ToListAsync();
      return ServiceResponse<List<AccountDto>>.Success(mapper.Map<List<AccountDto>>(accounts));
    }
    catch (Exception ex)
    {
      logger.LogError("Error retrieving accounts: " + ex.Message);
      return ServiceResponse<List<AccountDto>>.Failure(ex.Message);
    }
  }

  public async Task<ServiceResponse<bool>> AdminSetUserActiveStatus(int id, bool active)
  {
    try
    {
      var account = await context.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
      if (account == null) return ServiceResponse<bool>.Failure("Account not found");
      account.IsActive = active;
      context.Accounts.Update(account);
      await context.SaveChangesAsync();
      return ServiceResponse<bool>.Success(true);

    }
    catch (Exception e)
    {
      return ServiceResponse<bool>.Failure("Something went wrong");
    }
  }

  public async Task<ServiceResponse<bool>> AdminSetUserRole(int id, Role role)
  {
    try
    {
      var account = await context.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.Id == id);
      if (account == null) return ServiceResponse<bool>.Failure("Account not found");
      account.Role = role;
      context.Accounts.Update(account);
      await context.SaveChangesAsync();
      return ServiceResponse<bool>.Success(true);

    }
    catch (Exception e)
    {
      return ServiceResponse<bool>.Failure("Something went wrong");
    }
  }

  public Task<ServiceResponse<bool>> DeleteAccountAsync(int id)
  {
    throw new NotImplementedException();
  }

  public async Task<ServiceResponse<AccountDto>> UpdateAccountAsync(AccountDto request, Account account)
  {
    try
    {
      var updatedAccount = mapper.Map<Account>(request);
      updatedAccount.PrepareEntityForUpdate(account);
      updatedAccount.PasswordHash = account.PasswordHash;
      updatedAccount.IsActive = true;
      context.Update(updatedAccount);
      await context.SaveChangesAsync();
      return ServiceResponse<AccountDto>.Success(request);
      
    }
    catch (Exception e)
    {
      return ServiceResponse<AccountDto>.Failure("Something went wrong");
    }
  }

  public async Task<ServiceResponse<bool>> SendResetPasswordCode(string email)
  {
    await using var transaction = await context.Database.BeginTransactionAsync();
    try
    {
      var account = await context.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.Email == email);
      if (account == null) return ServiceResponse<bool>.Failure("Account not found");
      var code = string.Concat(Enumerable.Range(0, 6).Select(_ => Random.Shared.Next(0, 10)));
      account.ResetCode = code;
      account.ResetCodeExpTime = DateTime.Now.AddMinutes(30);
      context.Accounts.Update(account);
      await context.SaveChangesAsync();
      await emailService.Send(email, "Password Reset Code", code);
      await transaction.CommitAsync();
      return ServiceResponse<bool>.Success(true);
    }
    catch (Exception e)
    {
      await transaction.RollbackAsync();
      Console.WriteLine(e);
      throw;
    }
  }

  public async Task<ServiceResponse<bool>> VerifyResetCode(string code, string email)
  {
    try
    {
      var account = await context.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.Email == email);
      if (account == null) return ServiceResponse<bool>.Failure("Account not found");
      if(code == account.ResetCode && DateTime.Now < account.ResetCodeExpTime) return ServiceResponse<bool>.Success(true);
      if(code == account.ResetCode && account.ResetCodeExpTime < DateTime.Now) return ServiceResponse<bool>.Failure("Code expired");
      return ServiceResponse<bool>.Failure("Incorrect Reset Code");
    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      throw;
    }
  }

  public async Task<ServiceResponse<bool>> ResetPassword(string password, string email)
  {
    try
    {
      var hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
      var account = await context.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.Email == email);
      if(account == null) return ServiceResponse<bool>.Failure("Account not found");
      account.PasswordHash = hashedPassword;
      account.ResetCodeExpTime = null;
      account.ResetCode = null;
      context.Accounts.Update(account);
      await context.SaveChangesAsync();
      return ServiceResponse<bool>.Success(true);

    }
    catch (Exception e)
    {
      Console.WriteLine(e);
      throw;
    }
  }

  
}
