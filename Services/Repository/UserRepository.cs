using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiwiTalent.Models;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.Services.Repository
{
    public class UserRepository : IUserRepository
    {
        public Task<IEnumerable<User>> GetUsers()
        {
            throw new NotImplementedException();
        }
    }
}