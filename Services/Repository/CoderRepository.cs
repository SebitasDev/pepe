using AutoMapper;
using MongoDB.Driver;
using RiwiTalent.Infrastructure.Data;
using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.Services.Repository
{
    public class CoderRepository : ICoderRepository
    {
        private readonly IMongoCollection<Coder> _mongoCollection;
        private readonly IMapper _mapper; 
        private string Error = "The coder not found";
        public CoderRepository(MongoDbContext context, IMapper mapper)
        {
            _mongoCollection = context.Coders;
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

            await _mongoCollection.ReplaceOneAsync(filter, existCoder);
        
        }

        public void Delete(string id)
        {
            //This Method is the reponsable of update status the coder, first we search by id and then it execute the change Active to Inactive

            var filter = Builders<Coder>.Filter.Eq(c => c.Id, id);         
            var update = Builders<Coder>.Update.Set(c => c.Status, "Inactive");            
            _mongoCollection.UpdateOneAsync(filter, update);
        }

        public void ReactivateCoder(string id)
        {
            //This Method is the reponsable of update status the coder, first we search by id and then it execute the change Inactive to Active
            
            var filter = Builders<Coder>.Filter.Eq(c => c.Id, id);           
            var update = Builders<Coder>.Update.Set(c => c.Status, "Active");
            _mongoCollection.UpdateOne(filter, update);
        }
    }
}
