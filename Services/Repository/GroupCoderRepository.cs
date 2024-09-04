using MongoDB.Driver;
using RiwiTalent.Infrastructure.Data;
using RiwiTalent.Models;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.Services.Repository
{
    public class GroupCoderRepository : IGroupCoderRepository
    {
        private readonly IMongoCollection<GruopCoder> _mongoCollection;
        public GroupCoderRepository(MongoDbContext context)
        {
            _mongoCollection = context.GroupCoders;
        }
        public void add(GruopCoder groupCoder)
        {
            _mongoCollection.InsertOne(groupCoder);
        }

        public async Task<IEnumerable<GruopCoder>> GetGroupCoders()
        {
            var Groups = await _mongoCollection.Find(_ => true).ToListAsync();
            return Groups;
        }

        public async Task Update(GruopCoder groupCoder)
        {
            //we need filter groups by Id
            //First we call the method Builders and have access to Filter
            //Then we can use filter to have access Eq
            var builder = Builders<GruopCoder>.Filter;
            var filter = builder.Eq(group => group.Id, groupCoder.Id );

            await _mongoCollection.ReplaceOneAsync(filter, groupCoder);
        }
    }
}