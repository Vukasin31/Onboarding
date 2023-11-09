using PizzasApi.Services;
using PizzasApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using ContosoPizza.Service;

namespace ContosoPizza
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			builder.Services.Configure<PizzaDatabaseSettings>(
			builder.Configuration.GetSection(nameof(PizzaDatabaseSettings)));

			builder.Services.AddSingleton<IPizzaDatabaseSettings>(sp =>
				sp.GetRequiredService<IOptions<PizzaDatabaseSettings>>().Value);

			builder.Services.AddSingleton<PizzaDBService>();
			builder.Services.AddSingleton<IPizzaService, PizzaService>();

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
