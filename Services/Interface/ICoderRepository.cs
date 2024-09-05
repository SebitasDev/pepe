<<<<<<< HEAD
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
=======
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
>>>>>>> e66dee7 (feat: desarrollo logica de soft-delete-coders)
