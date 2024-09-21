using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;

namespace RiwiTalent.Services.Interface
{
    public interface ICoderRepository
    {
        Task<IEnumerable<Coder>> GetCoders();
        Task<Pagination<Coder>> GetCodersPagination(int page, int cantRegisters);
        void Add(Coder coder);
        Task Update(CoderDto coderDto);
        Task UpdateCodersGroup(CoderGroupDto gruopCoder);
        Task<Coder> GetCoderId(string id);
        Task<Coder> GetCoderName(string name);
        void Delete(string id);    
        void ReactivateCoder(string id);
    }
}
