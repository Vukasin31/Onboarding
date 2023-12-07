using ContosoPizza.Models;
using MongoDB.Driver;
using PizzasApi.Models;

namespace ContosoPizza.Service
{
    public class BeverageDBService
    {
        private readonly IMongoCollection<Beverage> _beverage;

        public BeverageDBService(PizzaDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            _beverage = database.GetCollection<Beverage>(settings.BeveragesCollectionName);
        }

        public List<Beverage> GetAll() =>
            _beverage.Find(beverage => true).ToList();

        public Beverage Get(string id) =>
            _beverage.Find(beverage => beverage.BeverageId == id).FirstOrDefault();

        public Beverage Create(Beverage beverage)
        {
            _beverage.InsertOne(beverage);
            return beverage;
        }

        public void Update(string id, Beverage beverageIn) =>
            _beverage.ReplaceOne(beverage => beverage.BeverageId == id, beverageIn);

        public void Remove(Beverage beverageIn) =>
            _beverage.DeleteOne(beverage => beverage.BeverageId == beverageIn.BeverageId);

        public void Remove(string id) =>
            _beverage.DeleteOne(beverage => beverage.BeverageId == id);
    }
}

