using System.Text;
using DoughManager.Data.DbContexts;
using DoughManager.Services.Helpers;
using DoughManager.Services.Interfaces;
using DoughManager.Services.Services;
using DoughManager.Services.Shared;
using GoldenDusk.Core.AppServices.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

namespace DoughManager.Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection2");
        builder.Services.AddDbContext<DoughManagerDbContext>((Action<DbContextOptionsBuilder>)(options =>
        {
            options.UseSqlServer(connectionString);
            if(builder.Environment.IsDevelopment()) options.EnableSensitiveDataLogging();
            
        }));
        if (builder.Environment.IsDevelopment())
        {
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(5000);
                options.ListenAnyIP(5001, listenOptions =>
                {
                    listenOptions.UseHttps();
                });
            });
        }
        
        builder.Services.AddTransient<IAccountService, AccountService>();
        builder.Services.AddTransient<IEmailService, EmailService>();
        builder.Services.AddTransient<IProductService, ProductService>();
        builder.Services.AddTransient<IRawMaterialService, RawMaterialService>();
        builder.Services.AddTransient<IJwtUtils, JwtUtils>();
        builder.Services.AddTransient<IOrderService, OrderService>();
        builder.Services.AddTransient<IDashboardService, DashboardService>();
        builder.Services.AddTransient<IProductionBatchService, ProductionBatchService>();
        builder.Services.AddTransient<IDispatchService, DispatchService>();
        builder.Services.AddTransient<IStockTakeService, StockTakeService>();
        builder.Services.AddTransient<IDiscrepancyService, DiscrepancyService>();
        builder.Services.Configure<AppSettings>((IConfiguration)builder.Configuration.GetSection("AppSettings"));
        builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        builder.Services.AddCors();
        builder.Services.AddAuthentication((Action<AuthenticationOptions>)(x =>
        {
            x.DefaultScheme = "Bearer";
            x.DefaultChallengeScheme = "Bearer";
            x.DefaultAuthenticateScheme = "Bearer";
        })).AddJwtBearer((Action<JwtBearerOptions>)(jwtOptions =>
        {
            jwtOptions.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = (SecurityKey)new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Secret").Value))
            };
            jwtOptions.MapInboundClaims = false;
        }));
        builder.Services.AddAuthorization();
        builder.Services.AddControllers();
        builder.Services.AddOpenApi();
        WebApplication webApplication = builder.Build();
        webApplication.MapOpenApi();
        webApplication.MapScalarApiReference((Action<ScalarOptions>)(options =>
        {
            options.Title = "Moringa Bakery API";
            options.Theme = ScalarTheme.Moon;
        }));
        webApplication.UseHttpsRedirection();
        webApplication.UseRouting();
        webApplication.UseCors((Action<CorsPolicyBuilder>)(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
        webApplication.UseAuthentication();
        webApplication.UseAuthorization();
        webApplication.UseMiddleware<JwtMiddleware>();
        webApplication.MapControllers();
        webApplication.Run();
    }
}