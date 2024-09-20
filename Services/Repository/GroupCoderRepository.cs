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
        private readonly IMongoCollection<Coder> _mongoCollectionCoder;
        private readonly ExternalKeyUtils _service;
        private readonly IMapper _mapper;

        private string Error = "The group not found";
        public GroupCoderRepository(MongoDbContext context, IMapper mapper, ExternalKeyUtils service)
        {
            _mongoCollection = context.GroupCoders;
            _mongoCollectionCoder = context.Coders;
            _mapper = mapper;
            _service = service;
        }
        public ObjectId Add(GruopCoder groupCoder, CoderDto coderDto)
        {
            ObjectId objectId = ObjectId.GenerateNewId();
            groupCoder.Id = objectId;
            Guid guid = _service.ObjectIdToUUID(objectId);


            //we define the path of url link
            string Link = $"http://riwitalent/external/{guid}";
            string tokenString = _service.GenerateTokenRandom();

            //define a new instance to add uuid into externalkeys -> url
            GruopCoder newGruopCoder = new GruopCoder
            {
                Id = objectId,
                Name = groupCoder.Name,
                Description = groupCoder.Description,
                Created_At = DateTime.UtcNow,
                Coders = new List<CoderDto>
                {
                    new CoderDto
                    {
                        Id = coderDto.Id,
                        FirstName = coderDto.FirstName,
                        SecondName = coderDto.Id,
                        FirstLastName = coderDto.FirstLastName,
                        SecondLastName = coderDto.SecondLastName,
                        Email = coderDto.Email,
                        Photo = coderDto.Photo,
                        Age = coderDto.Age,
                        Cv = coderDto.Cv,
                        Status = coderDto.Status,
                        Stack = coderDto.Stack,
                        StandarRiwi = coderDto.StandarRiwi,
                        Skills = coderDto.Skills,
                        LanguageSkills = coderDto.LanguageSkills,
                    }
                },
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
                },
            };

            _mongoCollection.InsertOne(newGruopCoder);

            return groupCoder.Id;
        }

        public async Task DeleteCoderGroup(string id)
        {
            var filterCoder = Builders<Coder>.Filter.Eq(coder => coder.Id, id);
            var updateStatusAndRelation = Builders<Coder>.Update.Combine(
                Builders<Coder>.Update.Set(coder => coder.Status, Status.Inactive.ToString()),
                Builders<Coder>.Update.Set(coder => coder.GroupId, null)
            );

            await _mongoCollectionCoder.UpdateOneAsync(filterCoder, updateStatusAndRelation);
        }

        public async Task<IEnumerable<GroupCoderDto>> GetGroupCoders()
        {
            var Groups = await _mongoCollection.Find(_ => true).ToListAsync();

            var newGroup = Groups.Select(groups => new GroupCoderDto
            {
                Id = groups.Id.ToString(),
                Name = groups.Name,
                Description = groups.Description,
                Created_At = groups.Created_At,
                ExternalKeys = groups.ExternalKeys
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