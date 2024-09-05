<<<<<<< HEAD
namespace RiwiTalent.Models
=======
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MongoDb.Models
>>>>>>> e66dee7 (feat: desarrollo logica de soft-delete-coders)
{
    public class ExternalKey
    {
        public string? Url { get; set; }
        public string? Key { get; set; }
        public DateTime Date_Creation { get; set; }
        public DateTime Date_Expiration { get; set; }
    }
}