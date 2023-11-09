using PizzasApi.Models;
using PizzasApi.Services;

namespace ContosoPizza.Service
{
	public interface IPizzaService
	{
		List<Pizza> GetPizzaAsync();
		Pizza GetPizzaAsync(string id);
		Pizza AddPizzaAsync(Pizza pizza);
		Pizza DeletePizzaAsync(Pizza pizza);
		Pizza UpdatePizzaAsync(Pizza pizza);
	}

	public class PizzaService : IPizzaService
	{
		private readonly PizzaDBService _pizzaDBService;

		public PizzaService(PizzaDBService dbService)
		{
			_pizzaDBService = dbService;
		}
		public List<Pizza> GetPizzaAsync()
		{
			var pizza = _pizzaDBService.GetAll();
			return pizza;
		}
		public Pizza GetPizzaAsync(string id)
		{
			var pizza =  _pizzaDBService.Get(id);
			return pizza;
		}
		public Pizza AddPizzaAsync(Pizza pizza)
		{
			_pizzaDBService.Create(pizza);
			return pizza;
		}		
		public Pizza UpdatePizzaAsync(Pizza pizza)
		{
			var pizzaFromDB = _pizzaDBService.Get(pizza.Id);
			if (pizzaFromDB == null) throw new Exception("Pizza not found");
			_pizzaDBService.Update(pizza.Id, pizza);
			return pizza;
		}
		public Pizza DeletePizzaAsync(Pizza pizza)
		{
			var pizzaFromDB = _pizzaDBService.Get(pizza.Id);
			if (pizzaFromDB == null) throw new Exception("Article not found");
			_pizzaDBService.Remove(pizza.Id);
			return pizzaFromDB;
		}		
	}
}
