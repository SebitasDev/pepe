using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;

namespace RiwiTalent.Services.Interface
{
    public interface IGroupCoderRepository 
    {
        Task<IEnumerable<GroupCoder>> GetGroupCoders(); //we get all groups
        void add(GroupCoder groupCoder);
        Task Update(GroupCoderDto groupCoderDto);
    }
}