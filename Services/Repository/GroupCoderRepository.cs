using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiwiTalent.Models;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.Services.Repository
{
    public class GroupCoderRepository : IGroupCoderRepository
    {
        public Task<IEnumerable<GroupCoder>> GetGroupCoders()
        {
            throw new NotImplementedException();
        }
    }
}