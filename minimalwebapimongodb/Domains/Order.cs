using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using minimalAPIMongo.Domains;

namespace minimalwebapimongodb.Domains
{
    public class Order
    {
        [BsonId]
        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? ID { get; set; }

        [BsonElement("date")]
        public DateTime? Date { get; set; }

        [BsonElement("status")]
        public bool Status { get; set; }

        [BsonElement("product")]
        public List<Product> Products { get; set; }

        [BsonElement("client")]
        public Client? Clients { get; set; }

        public Order()
        {
            Products = new List<Product>();
        }

    }
}
