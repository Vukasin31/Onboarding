using ContosoPizza.Models;
using ContosoPizza.Service;
using Microsoft.AspNetCore.Mvc;
using PizzasApi.Models;

namespace ContosoPizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeverageController : ControllerBase 
    {
        private readonly IBeverageService _beverageService;

        public BeverageController(IBeverageService beverageService)
        {
            _beverageService = beverageService;
        }

        [HttpGet(Name = "Get all beverages")]
        public ActionResult<List<Beverage>> Get()
        {
            var beverage = _beverageService.GetBeverage();
            return beverage;
        }


        [HttpGet("{id:length(24)}", Name = "Get beverage")]
        public ActionResult<Beverage> Get(string id)
        {
            var beverage = _beverageService.GetBeverage(id);
            return beverage;
        }

        [HttpPost(Name = "Create Beverage")]
        public ActionResult<Beverage> Create(Beverage beverage)
        {
            _beverageService.AddBeverage(beverage);
            return beverage;
        }

        [HttpPut("{id:length(24)}", Name = "Update beverage")]
        public ActionResult<Beverage> Update(string id, Beverage beverageIn)
        {
            _beverageService.UpdateBeverage(beverageIn);
            return beverageIn;
        }

        [HttpDelete("{id:length(24)}", Name = "Delete beverage")]
        public ActionResult Delete(string id)
        {
            _beverageService.DeleteBeverage(id);
            return Ok();
        }
    }

}
