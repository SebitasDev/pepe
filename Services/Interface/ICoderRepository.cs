using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiwiTalent.Models;

namespace RiwiTalent.Services.Interface
{
    public interface ICoderRepository
    {
        Task<IEnumerable<Coder>> GetCoders(); //we get the all coders
    }
}