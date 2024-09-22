using AutoMapper;
using MongoDB.Driver;
using RiwiTalent.Infrastructure.Data;
using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;
using RiwiTalent.Models.Enums;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.Services.Repository
{
    public class CoderRepository : ICoderRepository
    {
        private readonly IMongoCollection<Coder> _mongoCollection;
        private readonly IMongoCollection<GruopCoder> _mongoCollectionGroups;
        private readonly IMapper _mapper; 
        private string Error = "The coder not found";
        public CoderRepository(MongoDbContext context, IMapper mapper)
        {
            _mongoCollection = context.Coders;
            _mongoCollectionGroups = context.GroupCoders;
            _mapper = mapper;
        }

        public void Add(Coder coder)
        {
            _mongoCollection.InsertOne(coder);

        }

        public async Task<Coder> GetCoderId(string id)
        {
            //In this method we get coders by id and we do a control of errors.
            try
            {
                return await _mongoCollection.Find(Coders => Coders.Id == id).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Ocurrió un error al obtener el coder", ex);
            }
        }

        public async Task<Coder> GetCoderName(string name)
        {
            //In this method we get coders by name and we do a control of errors.
            try
            {
                return await _mongoCollection.Find(Coders => Coders.FirstName == name).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {
                
                throw new ApplicationException("Ocurrió un error al obtener el coder", ex);
            }
        }

        public async Task<IEnumerable<Coder>> GetCoders()
        {
            var coders = await _mongoCollection.Find(_ => true)
                                                .ToListAsync();
            
            return coders;
        }

        public async Task<Pagination<Coder>> GetCodersPagination(int page, int cantRegisters)
        {
            var skip = (page -1) * cantRegisters;
            //we get all coders
            var coders = await _mongoCollection.Find(_ => true)
                                                .Skip(skip)
                                                .Limit(cantRegisters)
                                                .ToListAsync();

            var total = await _mongoCollection.CountDocumentsAsync(_ => true);                                  
            return Pagination<Coder>.CreatePagination(coders, (int)total, page, cantRegisters);
        }

        public async Task Update(CoderDto coderDto)
        {
            //we need filter groups by Id
            //First we call the method Builders and have access to Filter
            //Then we can use filter to have access Eq

            var existCoder = await _mongoCollection.Find(coder => coder.Id == coderDto.Id).FirstOrDefaultAsync();

            if(existCoder is null)
            {
                throw new Exception($"{Error}");
            }

            var coderMap = _mapper.Map(coderDto, existCoder);
            var builder = Builders<Coder>.Filter;
            var filter = builder.Eq(coder => coder.Id, coderMap.Id);

            await _mongoCollection.ReplaceOneAsync(filter, coderMap);
        
        }  

        public void Delete(string id)
        {
            //This Method is the reponsable of update status the coder, first we search by id and then it execute the change Active to Inactive

            var filter = Builders<Coder>.Filter.Eq(c => c.Id, id);         
            var update = Builders<Coder>.Update.Set(c => c.Status, Status.Inactive.ToString());            
            _mongoCollection.UpdateOneAsync(filter, update);
        }

        public void ReactivateCoder(string id)
        {
            //This Method is the reponsable of update status the coder, first we search by id and then it execute the change Inactive to Active
            
            var filter = Builders<Coder>.Filter.Eq(c => c.Id, id);           
            var update = Builders<Coder>.Update.Set(c => c.Status, Status.Active.ToString());
            _mongoCollection.UpdateOne(filter, update);
        }

        public async Task<IEnumerable<Coder>> GetCodersByGroup(string name)
        {
            try
            {
                // first we get the name
                var group = await _mongoCollectionGroups.Find(g => g.Name == name).FirstOrDefaultAsync();

                if (group == null)
                {
                    throw new ApplicationException($"El grupo con el nombre '{name}' no existe.");
                }

                // then we compare the coder group id with the group id
                var filter = Builders<Coder>.Filter.Eq(c => c.GroupId, group.Id.ToString());

                var codersList = await _mongoCollection.Find(filter).ToListAsync();

                return codersList;
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al obtener los coders del grupo '{name}'", ex);
            }
        }
    }
}
