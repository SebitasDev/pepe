using RiwiTalent.Models;

namespace RiwiTalent.Services.Interface
{
    public interface IGroupCoderRepository 
    {
        Task<IEnumerable<GruopCoder>> GetGroupCoders(); //we get all groups
        void add(GruopCoder gruopCoder);
        Task Update(GruopCoder gruopCoder);
    }
}