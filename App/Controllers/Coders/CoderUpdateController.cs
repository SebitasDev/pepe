using Microsoft.AspNetCore.Mvc;
using MongoDb.Models;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.App.Controllers.Coders
{
    public class CoderUpdateController : Controller
    {
        private readonly ICoderRepository _coderRepository;
        public string Error = "Server Error: The request has not been resolve";
        public CoderUpdateController(ICoderRepository coderRepository)
        {
            _coderRepository = coderRepository;
        }

        //Endpoint
        [HttpPut]
        [Route("RiwiTalent/UpdateCoder")]
        public async Task<IActionResult> UpdateCoder(Coder coder)
        {
            if(coder == null)
            {
                return BadRequest("Invalida coder data or Id");
            }

            try
            {
                await _coderRepository.Update(coder);
                return Ok("The coder has been updated the correct way");
            }
            catch (Exception)
            {
                throw new Exception(Error);
            }
        }
    }
}