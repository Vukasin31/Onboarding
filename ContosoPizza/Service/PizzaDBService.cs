using PizzasApi.Models;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;

namespace PizzasApi.Services
{
	public class PizzaDBService
	{
		private readonly IMongoCollection<Pizza> _pizzas;

		public PizzaDBService(IPizzaDatabaseSettings settings)
		{
			var client = new MongoClient(settings.ConnectionString);
			var database = client.GetDatabase(settings.DatabaseName);

			_pizzas = database.GetCollection<Pizza>(settings.PizzasCollectionName);
		}

		public List<Pizza> GetAll() =>
			_pizzas.Find(pizza => true).ToList();

		public Pizza Get(string id) =>
			_pizzas.Find(pizza => pizza.Id == id).FirstOrDefault();

		public Pizza Create(Pizza pizza)
		{
			_pizzas.InsertOne(pizza);
			return pizza;
		}

		public void Update(string id, Pizza pizzaIn) =>
			_pizzas.ReplaceOne(pizza => pizza.Id == id, pizzaIn);

		public void Remove(Pizza pizzaIn) =>
			_pizzas.DeleteOne(pizza => pizza.Id == pizzaIn.Id);

		public void Remove(string id) =>
			_pizzas.DeleteOne(pizza => pizza.Id == id);
	}
}