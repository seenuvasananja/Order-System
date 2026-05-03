using Microsoft.EntityFrameworkCore;
using OrderService.API.Middleware;
using OrderService.Application.Interfaces;
using OrderService.Application.Services;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.Repositories;

namespace OrderService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
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

            //Global Exception Handling
            app.UseMiddleware<GlobalExceptionMiddleware>();
            app.MapControllers(); 
            app.Run();
        }
    }
}
