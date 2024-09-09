using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;

namespace RiwiTalent.Services.Interface
{
    public interface IGroupCoderRepository 
    {
        Task<IEnumerable<GruopCoder>> GetGroupCoders(); //we get all groups
        void add(GruopCoder groupCoder);
        Task Update(GroupCoderDto groupCoderDto);
    }
}