using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace ContosoPizza.Models
{
    public class Beverage
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string BeverageId { get; set; } = ObjectId.GenerateNewId().ToString();

        [BsonElement("BeverageName")]
        [JsonProperty("BeverageName")]

        public string? BeverageName { get; set; }
        public string? Containance { get; set; }
    }
}
