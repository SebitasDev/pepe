using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace RiwiTalent.Models.DTOs
{
  public class CoderGroupDto
  {
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string GruopId { get; set; }
    public List<string> CoderList {get; set;}
  }
}