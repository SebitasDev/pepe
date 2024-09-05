using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;

namespace RiwiTalent.Services.Interface
{
    public interface ICoderRepository
    {
        Task<IEnumerable<Coder>> GetCoders(); //we get the all coders
        void add(Coder coder);
        Task Update(CoderDto coderDto);//we Update a specific coder
    }
}