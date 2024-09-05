using RiwiTalent.Models;
using MongoDB.Driver;
using System.Text.RegularExpressions;

namespace RiwiTalent.Infrastructure.Data
{
    public class MongoDbContext
    {
        private readonly IMongoDatabase _database;
        public MongoDbContext(IConfiguration configuration)
        {
            //we realize the connection to Database
            var client = new MongoClient(configuration.GetConnectionString("DbConnection"));
            _database = client.GetDatabase(configuration["MongoDbSettings:Database"]);
        }

        //We define connection to Models
        public IMongoCollection<Coder> Coders => _database.GetCollection<Coder>("Coders");
        public IMongoCollection<GroupCoder> GroupCoders => _database.GetCollection<GroupCoder>("GroupCoders");
        public IMongoCollection<User> Users => _database.GetCollection<User>("Users");
    }
}