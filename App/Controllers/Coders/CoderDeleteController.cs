using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.App.Controllers.Coders
{
    public class CoderDeleteController : Controller
    {
        private readonly ICoderRepository _coderRepository;
        public string Error = "Server Error: The request has not been resolve";
        public CoderDeleteController(ICoderRepository coderRepository)
        {
            _coderRepository = coderRepository;
        }
    
        [HttpDelete]
        [Route("riwitalent/{id:length(24)}/deletecoder")]
        public IActionResult Delete(string id)
        {
            /* The function has the main principle of search by coder id
                and then update status the Active to Inactive
            */
            try
            {                
                _coderRepository.Delete(id);               
                return Ok(new { Message = "The status of coder has been updated to Inactive" });
            }
            catch (Exception)
            {
                return StatusCode(500, Error);
                throw;
            }
        }

        [HttpPut]
        [Route("riwitalent/{id:length(24)}/reactivate")]
        public IActionResult Reactivate(string id)
        {
            /* The function has the main principle of search by coder id
                and then update status the Inactive to Active
            */
            try
            {
                _coderRepository.ReactivateCoder(id);
                return Ok(new { Message = "The status of coder has been updated to Active" });
            }
            catch (Exception)
            {   
                return StatusCode(500, Error);
                throw;  
            }
        }
    }
}


    