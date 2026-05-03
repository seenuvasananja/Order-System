using Microsoft.EntityFrameworkCore;
using OrderService.API.Middleware;
using OrderService.Application.Interfaces;
using OrderService.Application.Services;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.Repositories;
using Serilog;

namespace OrderService.Api
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

            // 🔥 Plug Serilog 
            builder.Host.UseSerilog();

            // Add services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            // DB
            var useInMemory = builder.Configuration.GetValue<bool>("UseInMemory");

            if (useInMemory)
            {
                builder.Services.AddDbContext<AppDbContext>(opt =>
                    opt.UseInMemoryDatabase("OrderDb"));
            }
            else
            {
                builder.Services.AddDbContext<AppDbContext>(opt =>
                    opt.UseSqlServer(
                        builder.Configuration.GetConnectionString("DefaultConnection")));
            }

            // DI
            builder.Services.AddScoped<IOrderMangerRepository, OrderManagerRepository>();
            builder.Services.AddScoped<IOrderMangerService, OrderMangerService>();

            var app = builder.Build();

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseHttpsRedirection();
            app.UseAuthorization();

            // 🔥 Request logging
            app.UseSerilogRequestLogging();

            // 🔥 Correlation ID
            app.Use(async (context, next) =>
            {
                var requestId = Guid.NewGuid().ToString();

                context.Items["RequestId"] = requestId;

                using (Serilog.Context.LogContext.PushProperty("RequestId", requestId))
                {
                    await next();
                }
            });

            // Global Exception Handling
            app.UseMiddleware<GlobalExceptionMiddleware>();

            app.MapControllers();
            app.Run();
        }
    }
}