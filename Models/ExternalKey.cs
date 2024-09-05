using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace MongoDb.Models
{
    public class ExternalKey
    {
        public string? Url { get; set; }
        public string? Key { get; set; }
        public DateTime Date_Creation { get; set; }
        public DateTime Date_Expiration { get; set; }
    }
}