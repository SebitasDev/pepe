using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RiwiTalent.Models
{
    public class GroupCoder
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime Created_At { get; set; }
        public List<ExternalKey> ExternalKeys { get; set; } = new List<ExternalKey>();
    }
}