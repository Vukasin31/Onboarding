namespace PizzasApi.Models
{

    public class PizzaDatabaseSettings
    {
        public string PizzasCollectionName { get; set; }
        public string BeveragesCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }

}