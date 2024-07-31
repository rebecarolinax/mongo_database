using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace minimalAPIMongo.Domains
{
    public class User
    {
        [BsonId]
        [BsonIgnoreIfDefault]

        [BsonElement("_id"), BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonElement("name")]
        public string? Name { get; set; }

        [BsonElement("email")]
        public string? Email {  get; set; }

        [BsonElement("password")]
        public string? Password { get; set; }


        public Dictionary<string, string> AdditionalAtributes { get; set; }


        public User()
        {
            AdditionalAtributes = new Dictionary<string, string>();
        }
    }
}
