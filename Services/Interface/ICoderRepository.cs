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
        Task Update(CoderDto coderDto);//we Update a specific coder
        Task<Coder> GetCoderId(string id); //we get a specific coder by id
        Task<Coder> GetCoderName(string name); //we get a specific coder by Name
        void add(Coder coder);//we create a coder
    }
}
