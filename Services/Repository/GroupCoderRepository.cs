using AutoMapper;
using MongoDB.Bson;
using MongoDB.Driver;
using RiwiTalent.Infrastructure.Data;
using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;
using RiwiTalent.Models.Enums;
using RiwiTalent.Services.Interface;
using RiwiTalent.Utils.ExternalKey;

namespace RiwiTalent.Services.Repository
{
    public class GroupCoderRepository : IGroupCoderRepository
    {
        private readonly IMongoCollection<GruopCoder> _mongoCollection;
                private readonly ExternalKeyUtils _service;
        private readonly IMapper _mapper;

        private string Error = "The group not found";
        public GroupCoderRepository(MongoDbContext context, IMapper mapper, ExternalKeyUtils service)
        {
            _mongoCollection = context.GroupCoders;
            _mapper = mapper;
            _service = service;
        }
        public ObjectId Add(GruopCoder groupCoder)
        {
            ObjectId objectId = ObjectId.GenerateNewId();
            groupCoder.Id = objectId;
            Guid guid = _service.ObjectIdToUUID(objectId);


            //we define the path of url link
            string Link = $"http://riwitalent/external/{guid}";
            string tokenString = _service.GenerateTokenRandom();

            foreach (var coder in groupCoder.Coders)
            {
                coder.Status = Status.Grouped.ToString();
            }

            //define a new instance to add uuid into externalkeys -> url
            GruopCoder newGruopCoder = new GruopCoder
            {
                Id = objectId,
                Name = groupCoder.Name,
                Description = groupCoder.Description,
                Created_At = DateTime.UtcNow,
                Coders = groupCoder.Coders,
                ExternalKeys = new List<ExternalKey>
                {
                    new ExternalKey
                    {
                        Url = Link,
                        Key = tokenString,
                        Status = Status.Active.ToString(),
                        Date_Creation = DateTime.UtcNow,
                        Date_Expiration = DateTime.UtcNow.AddDays(7)
                    }
                }
            };

            _mongoCollection.InsertOne(newGruopCoder);

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