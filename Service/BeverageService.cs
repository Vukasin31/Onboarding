using AutoMapper;
using ContosoPizza.Models;

namespace ContosoPizza.Service
{

    public interface IBeverageService
    {
        List<Beverage> GetBeverage();
        Beverage GetBeverage(string id);
        Beverage AddBeverage(Beverage beverage);
        void DeleteBeverage(string id);
        Beverage UpdateBeverage(Beverage beverage);
    }

    public class BeverageService : IBeverageService
    {
        private readonly BeverageDBService _beverageDBService;
        private readonly IMemoryService _memoryPizzaService;
        private readonly IMapper _mapper;

        public BeverageService(BeverageDBService dbService, IMemoryService memoryService, IMapper mapper)
        {
            _beverageDBService = dbService;
            _memoryPizzaService = memoryService;
            _mapper = mapper;
        }
        public List<Beverage> GetBeverage()
        {
            var beverage = _beverageDBService.GetAll();
            return beverage;
        }
        public Beverage GetBeverage(string id)
        {
            var beverage = _beverageDBService.Get(id);
            return beverage;
        }
        public Beverage AddBeverage(Beverage beverage)
        {
            _beverageDBService.Create(beverage);
            var metaData = _mapper.Map<MetaDataObject>(beverage);
            _memoryPizzaService.Backup(beverage);
            return beverage;
        }
        public Beverage UpdateBeverage(Beverage beverage)
        {
            var beverageFromDB = _beverageDBService.Get(beverage.BeverageId);
            if (beverageFromDB == null) throw new Exception("Beverage not found");
            _beverageDBService.Update(beverage.BeverageId, beverage);
            return beverage;
        }
        public void DeleteBeverage(string id)
        {
            var beverage = _beverageDBService.Get(id);
            if (beverage == null) throw new Exception("Article not found");

            _memoryPizzaService.Backup(beverage);
            _memoryPizzaService.UploadBlob(beverage);
            _beverageDBService.Remove(beverage.BeverageId);
        }
    }

}
