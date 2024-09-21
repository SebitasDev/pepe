using MongoDB.Bson;
using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;

namespace RiwiTalent.Services.Interface
{
    public interface IGroupCoderRepository 
    {
        Task<IEnumerable<GroupCoderDto>> GetGroupCoders();
        Task<GroupInfoDto> GetGroupInfoById(string groupId);
        ObjectId Add(GroupDto groupDto);
        Task Update(GroupCoderDto groupCoderDto);
        Task DeleteCoderGroup(string id);

    }
}