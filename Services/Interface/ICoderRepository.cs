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
        Task<IEnumerable<Coder>> GetCoders();
        Task<Pagination<Coder>> GetCodersPagination(int page, int cantRegisters); //we get the all coders
        void add(Coder coder);//we create a coder
        Task Update(CoderDto coderDto);//we Update a specific coder
        Task<Coder> GetCoderId(string id); //we get a specific coder by id
        Task<Coder> GetCoderName(string name); //we get a specific coder by Name
        void delete(string id);//delete a specific coder    
        void ReactivateCoder(string id);//re-activate a specific coder
        Task<List<Coder>> GetCodersByStack(List<string> stack);
        Task<List<Coder>> GetCodersBylanguage(string level);
    }
}
