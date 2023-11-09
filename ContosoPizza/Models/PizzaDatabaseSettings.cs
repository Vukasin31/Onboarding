namespace PizzasApi.Models
{
	public class PizzaDatabaseSettings : IPizzaDatabaseSettings
	{
		public string PizzasCollectionName { get; set; }
		public string ConnectionString { get; set; }
		public string DatabaseName { get; set; }
	}

	public interface IPizzaDatabaseSettings
	{
		string PizzasCollectionName { get; set; }
		string ConnectionString { get; set; }
		string DatabaseName { get; set; }
	}
}