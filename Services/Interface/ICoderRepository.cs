using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MongoDb.Models;
using RiwiTalent.Models;

namespace RiwiTalent.Services.Interface
{
    public interface ICoderRepository
    {
        Task<IEnumerable<Coder>> GetCoders(); //we get the all coders
        Task<Coder> GetCoderId(string id); //we get a specific coder by id
        Task<Coder> GetCoderName(string name); //we get a specific coder by Name
        void add(Coder coder);//we create a coder
        Task Update(Coder coder);//we Update a specific coder
    }
}