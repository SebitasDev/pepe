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
        void add(Coder coder);
        Task Update(Coder coder);//we Update a specific coder
        /*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*/
        void delete(string id);//delete a specific coder    
        void ReactivateCoder(string id);//re-activate a specific coder
        /*-*-*-*--*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*/
    }
}