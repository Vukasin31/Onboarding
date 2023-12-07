using AutoMapper;
using ContosoPizza.Models;
using ContosoPizza.Service;
using Microsoft.Extensions.Options;
using PizzasApi.Models;
using PizzasApi.Services;

namespace ContosoPizza
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.Configure<PizzaDatabaseSettings>(
            builder.Configuration.GetSection(nameof(PizzaDatabaseSettings)));

            builder.Services.AddAutoMapper(typeof(AutoMapper.Mapper));
            
            builder.Services.AddSingleton<IMemoryService, MemoryService>();
            builder.Services.AddSingleton(sp => sp.GetRequiredService<IOptions<PizzaDatabaseSettings>>().Value);

            builder.Services.AddSingleton<PizzaDBService>();
            builder.Services.AddSingleton<IPizzaService, PizzaService>();

            builder.Services.AddSingleton<BeverageDBService>();
            builder.Services.AddSingleton<IBeverageService, BeverageService>();

            builder.Services.AddControllers()
                .AddNewtonsoftJson(options => options.UseMemberCasing());

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
