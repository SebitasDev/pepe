<<<<<<< HEAD
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
>>>>>>> e66dee7 (feat: desarrollo logica de soft-delete-coders)

namespace MongoDb.Models
{
    public class GruopCoder
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