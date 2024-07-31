using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace minimalAPIMongo.Domains
{
    public class Client
        
    {
        [BsonId]
        [BsonIgnoreIfDefault]

        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("userId")]
        public string? UserId { get; set; }

        [BsonElement("CPF")]
        public string? CPF { get; set; }

        [BsonElement("phone")]
        public string? Phone { get; set; }

        [BsonElement("adress")]
        public string? Adress { get; set; }


        public Dictionary<string, string> AdditionalAtributes { get; set; }


        public Client()
        {
            AdditionalAtributes = new Dictionary<string, string>();
        }

    }
}
