using Microsoft.EntityFrameworkCore;
using OrderService.Application.Interfaces;
using OrderService.Infrastructure.Data;
using OrderService.Infrastructure.Repositories;
using OrderService.Application.Services;

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
            builder.Services.AddScoped<IOrderRepository, OrderRepository>();
            builder.Services.AddScoped<IOrderService, OrderManager>();


            var app = builder.Build();

            app.UseSwagger(); 
            app.UseSwaggerUI();

            app.UseHttpsRedirection(); 
            app.UseAuthorization(); 
            app.MapControllers(); 
            app.Run();
        }
    }
}
