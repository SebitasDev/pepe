using AutoMapper;
using MongoDB.Bson;
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
        public ObjectId Add(GruopCoder groupCoder)
        {
            _mongoCollection.InsertOne(groupCoder);

            return groupCoder.Id;
        }

        public async Task<IEnumerable<GroupCoderDto>> GetGroupCoders()
        {
            var Groups = await _mongoCollection.Find(_ => true).ToListAsync();

            var newGroup = Groups.Select(group => new GroupCoderDto
            {
                Id = group.Id.ToString(),
                Name = group.Name,
                Description = group.Description,
                Created_At = group.Created_At,
                Coders = group.Coders,
                ExternalKeys = group.ExternalKeys
            });
            return newGroup;
        }

        public async Task Update(GroupCoderDto groupCoderDto)
        {
            //we need filter groups by Id
            //First we call the method Builders and have access to Filter
            //Then we can use filter to have access Eq

            var convertIdToObjectId = ObjectId.Parse(groupCoderDto.Id);

            var existGroup = await _mongoCollection.Find(group => group.Id == convertIdToObjectId).FirstOrDefaultAsync();

            if(existGroup == null)
            {
                throw new Exception($"{Error}");
            }

            var groupCoders = _mapper.Map(groupCoderDto, existGroup);
            var builder = Builders<GruopCoder>.Filter;
            var filter = builder.Eq(group => group.Id, convertIdToObjectId );

            await _mongoCollection.ReplaceOneAsync(filter, groupCoders);
        }
    }
}