using RiwiTalent.Models;

namespace RiwiTalent.Services.Interface
{
    public interface ITokenRepository
    {
        string GetToken(User user);
    }
}