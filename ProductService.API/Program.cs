using Microsoft.EntityFrameworkCore;
using ProductService.API.Middleware;
using ProductService.Application.Interfaces;
using ProductService.Application.Services;
using ProductService.Infrastructure.Data;
using ProductService.Infrastructure.Repositories;
using Serilog;

namespace ProductService.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // 🔥 Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.File(
                    path: "logs/log-.txt",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 7,
                    outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] [{RequestId}] {Message}{NewLine}{Exception}"
                )
                .WriteTo.File(
                    path: "logs/error-.txt",
                    rollingInterval: RollingInterval.Day,
                    restrictedToMinimumLevel: Serilog.Events.LogEventLevel.Error
                )
                .CreateLogger();

            // 🔥 Plug Serilog into ASP.NET Core
            builder.Host.UseSerilog();

            // Add services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // 🔥 DB Configuration (same as OrderService)
            var useInMemory = builder.Configuration.GetValue<bool>("UseInMemory");

            if (useInMemory)
            {
                builder.Services.AddDbContext<AppDbContext>(opt =>
                    opt.UseInMemoryDatabase("ProductDb"));
            }
            else
            {
                builder.Services.AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlServer(
                        builder.Configuration.GetConnectionString("DefaultConnection")));
            }

            // 🔥 DI
            builder.Services.AddScoped<IProductManagerRepository, ProductManagerRepository>();
            builder.Services.AddScoped<IProductManagerService, ProductManagerService>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            // 🔥 Request logging
            app.UseSerilogRequestLogging();

            // 🔥 Correlation ID (same as OrderService)
            app.Use(async (context, next) =>
            {
                var requestId = Guid.NewGuid().ToString();

                context.Items["RequestId"] = requestId;

                using (Serilog.Context.LogContext.PushProperty("RequestId", requestId))
                {
                    await next();
                }
            });

            // 🔥 Global Exception Handling
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.MapControllers();
            app.Run();
        }
    }
}