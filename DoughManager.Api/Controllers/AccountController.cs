// Decompiled with JetBrains decompiler
// Type: MoringaBakery.Api.Controllers.AccountController
// Assembly: MoringaBakery.Api, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 4B2D6541-2FB5-408C-932B-3F249436BB09
// Assembly location: C:\Users\munas\OneDrive\Desktop\moringaBakery (2)\moringaBakery\MoringaBakery.Api.dll

#nullable enable
using DoughManager.Data.Shared;
using DoughManager.Services.Dtos.Auth;
using DoughManager.Services.Interfaces;
using DoughManager.Services.Shared;
using Microsoft.AspNetCore.Mvc;

namespace DoughManager.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController(IAccountService accountService) : BaseController
{
  [HttpPost("create")]
  public async Task<ActionResult<ServiceResponse<bool>>> CreateAsync(CreateAccountRequest request)
  {
    var response = await accountService.CreateAccountAsync(request);
    return Ok(response);
  }

  [HttpPost("login")]
  public async Task<ActionResult<ServiceResponse<AuthenticationResponse>>> LoginAsync(
    AuthenticationRequest request)
  {
    var response = await accountService.LoginAsync(request);
    return Ok(response);
  }
  [HttpPut("update")]
  public async Task<ActionResult<ServiceResponse<AccountDto>>> UpdateAsync(
    AccountDto request)
  {
    var response = await accountService.UpdateAccountAsync(request, Account);
    return Ok(response);
  }

  [HttpGet("getCurrentUser")]
  public async Task<ActionResult<ServiceResponse<AccountDto>>> GetUserAsync()
  {
    var response = await accountService.GetAccountAsync(Account);
    return Ok(response);
  }

  [HttpGet("getAllUsers")]
  public async Task<ActionResult<ServiceResponse<AccountDto>>> GetAllUsersAsync()
  {
    var response = await accountService.GetAllAccountsAsync();
    return Ok(response);
  }
  
  [HttpGet("changeActiveStatus")]
  public async Task<ActionResult<ServiceResponse<bool>>> ChangeActiveStatus([FromQuery] int id, [FromQuery] bool status)
  {
    var response = await accountService.AdminSetUserActiveStatus(id, status);
    return Ok(response);
  }
  
  [HttpGet("changeRole")]
  public async Task<ActionResult<ServiceResponse<bool>>> ChangeRole([FromQuery] int id, [FromQuery] Role role)
  {
    var response = await accountService.AdminSetUserRole(id, role);
    return Ok(response);
  }
  
  [HttpGet("getCode")]
  public async Task<ActionResult<ServiceResponse<bool>>> GetCode([FromQuery] string email)
  {
    var response = await accountService.SendResetPasswordCode(email);
    return Ok(response);
  }
  
  [HttpGet("verifyCode")]
  public async Task<ActionResult<ServiceResponse<bool>>> Verify([FromQuery] string email, [FromQuery] string code)
  {
    var response = await accountService.VerifyResetCode(code, email);
    return Ok(response);
  }
  
  [HttpGet("resetPassword")]
  public async Task<ActionResult<ServiceResponse<bool>>> ResetPassword([FromQuery] string email, [FromQuery] string password)
  {
    var response = await accountService.ResetPassword(password, email);
    return Ok(response);
  }
  
  
}
