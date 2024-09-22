using MongoDB.Bson;
using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;

namespace RiwiTalent.Services.Interface
{
    public interface ICoderStatusHistoryRepository
    {
        Task<IEnumerable<CoderStatusHistory>> GetCodersStatus();
        Task<IEnumerable<CoderStatusHistory>> GetCompanyGroupedCoders(string id);
        // Task<Pagination<Coder>> GetCodersPagination(int page, int cantRegisters);
        // void Add(CoderStatusHistory coder);
    }
}
