using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace PizzasApi.Models
{
	public class Pizza
	{
		[BsonId]
		[BsonRepresentation(BsonType.ObjectId)]
		public string Id { get; set; } =  ObjectId.GenerateNewId().ToString();

		[BsonElement("PizzaName")]
		[JsonProperty("PizzaName")]
		
		public string PizzaName { get; set; }
		public bool IsGlutenFree { get; set; }
		
	}
}
