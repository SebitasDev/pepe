using MongoDB.Bson;
using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;

namespace RiwiTalent.Services.Interface
{
    public interface IGroupCoderRepository 
    {
        Task<IEnumerable<GruopCoder>> GetGroupCoders();
        ObjectId Add(GruopCoder groupCoder);
        Task Update(GruopCoder groupCoder);
    }
}