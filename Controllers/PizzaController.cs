using ContosoPizza.Models;
using ContosoPizza.Service;
using Microsoft.AspNetCore.Mvc;
using PizzasApi.Models;
using PizzasApi.Services;


namespace ContosoPizza.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class PizzasController : ControllerBase
	{		
		private readonly IPizzaService _pizzaService;
		
        public PizzasController(IPizzaService pizzaService)
        {

            _pizzaService = pizzaService;
            
        }

        [HttpGet(Name = "Get all Pizzas")]
		public ActionResult<List<Pizza>> Get()
		{
			var pizza = _pizzaService.GetPizza();
			return pizza;
		}


		[HttpGet("{id:length(24)}", Name = "GetPizza")]
		public ActionResult<Pizza> Get(string id)
		{
			var pizza = _pizzaService.GetPizza(id);
			return pizza;
		}

		[HttpPost(Name = "Create Pizza")]
		public ActionResult<Pizza> Create(Pizza pizza)
		{
			_pizzaService.AddPizza(pizza);
			return pizza;
		}

		[HttpPut("{id:length(24)}", Name = "Update Pizza")]
		public ActionResult<Pizza> Update(string id, Pizza pizzaIn)
		{
			_pizzaService.UpdatePizza(pizzaIn);
			return pizzaIn;
		}

		[HttpDelete("{id:length(24)}", Name = "Delete Pizza")]
		public ActionResult Delete(string id)
		{
			_pizzaService.DeletePizza(id);			
			return Ok();
		}

	}
}
