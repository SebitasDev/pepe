using MongoDb.Models;
using MongoDB.Driver;
using RiwiTalent.Infrastructure.Data;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.Services.Repository
{
    public class CoderRepository : ICoderRepository
    {
        private readonly IMongoCollection<Coder> _mongoCollection;
        public CoderRepository(MongoDbContext context)
        {
            _mongoCollection = context.Coders;
        }
        public void add(Coder coder)
        {
            _mongoCollection.InsertOne(coder);

        }

        
        public async Task<IEnumerable<Coder>> GetCoders()
        {
            var coder = await _mongoCollection.Find(_ => true).ToListAsync();
            return coder;
        }

        public async Task Update(Coder coder)
        {
            
            var filter = Builders<Coder>.Filter.Eq(c => c.Id, coder.Id);
            /* var UpdateCoder = Builders<Coder>.Update
                            .Set(c => c.FirstName, coder.FirstName)
                            .Set(c => c.SecondName, coder.SecondName)
                            .Set(c => c.FirstLastName, coder.FirstLastName)
                            .Set(c => c.SecondLastName, coder.SecondLastName)
                            .Set(c => c.Email, coder.Email)
                            .Set(c => c.Photo, coder.Photo)
                            .Set(c => c.Age, coder.Age)
                            .Set(c => c.Cv, coder.Cv)
                            .Set(c => c.Date_Update, DateTime.Now)
                            .Set(c => c.Status, coder.Status)
                            .Set(c => c.Stack, coder.Stack)
                            .Set(c => c.Skills, coder.Skills)
                            .Set(c => c.LanguageSkills, coder.LanguageSkills)
                            .Set(c => c.GroupId, coder.GroupId);

            _mongoCollection.UpdateOne(filter, UpdateCoder); */

            await _mongoCollection.ReplaceOneAsync(filter, coder);
        
        }       
      

        

        /*-----------------------------------------------------------------*/
        //==> Método para "eliminar" un coder, pero solo cambiando su estado a "Inactive"*/
        public void delete(string id)
        {             
            var filter = Builders<Coder>.Filter.Eq(c => c.Id, id); // Definimos el filtro para encontrar el coder por su Id            
            var update = Builders<Coder>.Update.Set(c => c.Status, "Inactive");// Definimos la actualización que cambia el estado a "inactivo"            
            _mongoCollection.UpdateOneAsync(filter, update);// Realizamos la actualización en la base de datos
        }
        /*-----------------------------------------------------------------*/
        //==> Método para "reactivar" un coder, cambiando su estado a "activo"*/
        public void ReactivateCoder(string id)
        {
            
            var filter = Builders<Coder>.Filter.Eq(c => c.Id, id);// Definimos un filtro para buscar el Coder por su Id            
            var update = Builders<Coder>.Update.Set(c => c.Status, "Active"); // Definimos la actualización que cambia el estado a "Active"
            _mongoCollection.UpdateOne(filter, update);// Realizamos la actualización en la base de datos de forma sincrónica
        }
        /*-----------------------------------------------------------------*/

    }

}
