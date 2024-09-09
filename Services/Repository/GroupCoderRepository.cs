using AutoMapper;
using MongoDB.Driver;
using RiwiTalent.Infrastructure.Data;
using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.Services.Repository
{
    public class GroupCoderRepository : IGroupCoderRepository
    {
        private readonly IMongoCollection<GruopCoder> _mongoCollection;
        private readonly IMapper _mapper;

        private string Error = "The group not found";
        public GroupCoderRepository(MongoDbContext context, IMapper mapper)
        {
            _mongoCollection = context.GroupCoders;
            _mapper = mapper;
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

        public async Task Update(GroupCoderDto groupCoderDto)
        {
            //we need filter groups by Id
            //First we call the method Builders and have access to Filter
            //Then we can use filter to have access Eq

            var existGroup = await _mongoCollection.Find(group => group.Id == groupCoderDto.Id).FirstOrDefaultAsync();

            if(existGroup == null)
            {
                throw new Exception($"{Error}");
            }

            var groupCoder = _mapper.Map(groupCoderDto, existGroup);
            var builder = Builders<GruopCoder>.Filter;
            var filter = builder.Eq(group => group.Id, groupCoder.Id );

            await _mongoCollection.ReplaceOneAsync(filter, groupCoder);
        }
    }
}