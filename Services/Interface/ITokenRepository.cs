using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiwiTalent.Models;

namespace backend.Services.Interface
{
    public interface ITokenRepository
    {
        string GetToken(User user);
    }
}