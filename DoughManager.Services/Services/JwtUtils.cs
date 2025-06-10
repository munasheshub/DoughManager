// Decompiled with JetBrains decompiler
// Type: DoughManager.Core.Services.JwtUtils
// Assembly: DoughManager.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: DECB816C-BF07-4FD4-B440-96F5925BA1E7
// Assembly location: C:\Users\munas\OneDrive\Desktop\DoughManager (2)\DoughManager\DoughManager.Core.dll

using DoughManager.Services.Interfaces;
using DoughManager.Services.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using DoughManager.Data.DbContexts;
using DoughManager.Data.EntityModels;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

#nullable enable
namespace DoughManager.Services.Services;

public class JwtUtils : IJwtUtils
{
  private readonly DoughManagerDbContext _context;
  private readonly AppSettings _appSettings;

  public JwtUtils(DoughManagerDbContext dbContext, IOptions<AppSettings> appSettings)
  {
        _context = dbContext;
        _appSettings = appSettings.Value;
  }

  public string GenerateJwtToken(Account account)
  {
    JwtSecurityTokenHandler securityTokenHandler = new JwtSecurityTokenHandler();
    byte[] bytes = Encoding.ASCII.GetBytes(_appSettings.Secret);
    List<Claim> claimList = new List<Claim>()
    {
      new Claim("name", account.UserName),
      new Claim("id", account.Id.ToString())
    };
    return securityTokenHandler.WriteToken(securityTokenHandler.CreateToken(new SecurityTokenDescriptor()
    {
      Subject = new ClaimsIdentity( claimList),
      Expires = new DateTime?(DateTime.UtcNow.AddHours(15.0)),
      SigningCredentials = new SigningCredentials((SecurityKey) new SymmetricSecurityKey(bytes), "http://www.w3.org/2001/04/xmldsig-more#hmac-sha256")
    }));
  }

  public async Task<Account?> ValidateJwtToken(string token)
  {
    if (token == null)
      return null;

    var tokenHandler = new JwtSecurityTokenHandler();
    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
    try
    {
      tokenHandler.ValidateToken(token, new TokenValidationParameters
      {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false,
        
        ClockSkew = TimeSpan.Zero
      }, out SecurityToken validatedToken);

      var jwtToken = (JwtSecurityToken)validatedToken;
      var userId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

     
      var account = await _context.Accounts.AsNoTracking().FirstOrDefaultAsync(a => a.Id == userId && !a.IsDeleted && a.IsActive);
      return account;
    }
    catch
    {
      // return null if validation fails
      return null;
    }
   
  }
}
