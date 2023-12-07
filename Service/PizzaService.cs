using AutoMapper;
using ContosoPizza.AutoMapper;
using ContosoPizza.Models;
using PizzasApi.Services;

namespace ContosoPizza.Service
{
    public interface IPizzaService
    {
        List<Pizza> GetPizza();
        Pizza GetPizza(string id);
        Pizza AddPizza(Pizza pizza);
        void DeletePizza(string id);
        Pizza UpdatePizza(Pizza pizza);
    }

    public class PizzaService : IPizzaService
    {
        private readonly PizzaDBService _pizzaDBService;
        private readonly IMemoryService _memoryPizzaService;
        private readonly IMapper _mapper;
        public PizzaService(PizzaDBService dbService, IMemoryService memoryPizzaService, IMapper mapper)
        {
            _pizzaDBService = dbService;
            _memoryPizzaService = memoryPizzaService;
            _mapper = mapper;
        }
        public List<Pizza> GetPizza()
        {
            var pizza = _pizzaDBService.GetAll();
            return pizza;
        }
        public Pizza GetPizza(string id)
        {
            var pizza = _pizzaDBService.Get(id);
            return pizza;
        }
        public Pizza AddPizza(Pizza pizza)
        {
            _pizzaDBService.Create(pizza);
            var metaData = _mapper.Map<MetaDataObject>(pizza);
            _memoryPizzaService.Backup(pizza);
            return pizza;
        }
        public Pizza UpdatePizza(Pizza pizza)
        {
            var pizzaFromDB = _pizzaDBService.Get(pizza.Id);
            if (pizzaFromDB == null) throw new Exception("Pizza not found");
            _pizzaDBService.Update(pizza.Id, pizza);
            return pizza;
        }
        public void DeletePizza(string id)
        {
            var pizza = _pizzaDBService.Get(id);
            if (pizza == null) throw new Exception("Article not found");

            _memoryPizzaService.Backup(pizza);
            _memoryPizzaService.UploadBlob(pizza);
            _pizzaDBService.Remove(pizza.Id);
        }
    }
}
