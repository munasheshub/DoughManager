using System.Text;
using System.Text.Json;
using DoughManager.Data.DbContexts;
using DoughManager.Services.Helpers;
using DoughManager.Services.Interfaces;
using DoughManager.Services.Services;
using DoughManager.Services.Shared;
using GoldenDusk.Core.AppServices.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;

namespace DoughManager.Api;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
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
                // options.ListenAnyIP(5001, listenOptions =>
                // {
                //     listenOptions.UseHttps();
                // });
            });
        }

        builder.Services.AddHealthChecks();
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
        WebApplication app = builder.Build();
        app.MapOpenApi();
        app.MapScalarApiReference((Action<ScalarOptions>)(options =>
        {
            options.Title = "Dough Manager API";
            options.Theme = ScalarTheme.Moon;
        }));
        app.MapHealthChecks("/health", new HealthCheckOptions
        {
            ResponseWriter = async (context, report) =>
            {
                context.Response.ContentType = "application/json";
                var result = JsonSerializer.Serialize(new
                {
                    status = report.Status.ToString(),
                    checks = report.Entries.Select(e => new {
                        name = e.Key,
                        status = e.Value.Status.ToString(),
                        exception = e.Value.Exception?.Message,
                        duration = e.Value.Duration.ToString()
                    })
                });
                await context.Response.WriteAsync(result);
            }
        });

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors((Action<CorsPolicyBuilder>)(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseMiddleware<JwtMiddleware>();
        app.MapControllers();
        app.Run();
    }
}