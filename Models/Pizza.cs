using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace ContosoPizza.Models
{
    public class Pizza
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("PizzaName")]
        [JsonProperty("PizzaName")]

        public string? PizzaName { get; set; }
        public bool IsGlutenFree { get; set; }
    }
}
