using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RiwiTalent.Models.DTOs
{
    public class GroupInfoDto
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public List<CoderDto>? Coders {get; set;}
    }
}