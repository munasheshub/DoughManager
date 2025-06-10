// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Helpers.JwtMiddleware
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Services.Interfaces;
using DoughManager.Services.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using DoughManager.Data.DbContexts;
using DoughManager.Data.EntityModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

#nullable enable
namespace DoughManager.Services.Helpers;

public class JwtMiddleware
{
  private readonly RequestDelegate _next;
  private readonly AppSettings _appSettings;

  public JwtMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
  {
        _next = next;
        _appSettings = appSettings.Value;
  }

  public async Task InvokeAsync(
    HttpContext context,
    DoughManagerDbContext dataContext,
    IJwtUtils jwtUtils)
  {
    string str = context.Request.Headers["Authorization"].FirstOrDefault<string>();
    Account account = await jwtUtils.ValidateJwtToken(str != null ?  str.Split(" ").Last() :  null);
    if (account != null)
      context.Items[(object) "Account"] = (object) account;
    await this._next(context);
  }
}
