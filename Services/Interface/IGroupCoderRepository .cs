using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;

namespace RiwiTalent.Services.Interface
{
    public interface IGroupCoderRepository 
    {
        Task<IEnumerable<GruopCoder>> GetGroupCoders();
        void Add(GruopCoder groupCoder);
        Task Update(GroupCoderDto groupCoderDto);
    }
}