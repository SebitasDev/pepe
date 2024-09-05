<<<<<<< HEAD
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
>>>>>>> e66dee7 (feat: desarrollo logica de soft-delete-coders)
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace RiwiTalent.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }
<<<<<<< HEAD
        public string? Name { get; set; }
=======
>>>>>>> e66dee7 (feat: desarrollo logica de soft-delete-coders)
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}