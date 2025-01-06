using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace PDF.Models
{
    public class AppUser
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
    }
}
