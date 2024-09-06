using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Models;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.App.Controllers.Coders
{
    public class CoderCreateController : Controller
    {
        private readonly ICoderRepository _coderRepository;
        public string Error = "Server Error: The request has not been resolve";
        public CoderCreateController(ICoderRepository coderRepository)
        {
            _coderRepository = coderRepository;
        }

        //Endpoint
        [HttpPost]
        [Route("RiwiTalent/CreateCoders")]
        public IActionResult Post([FromBody] Coder coder)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest(Utils.Exceptions.StatusError.CreateBadRequest());
            }

            try
            {
                _coderRepository.add(coder);
                return Ok("The coder has been created successfully");
            }
            catch (Exception)
            {
                throw new Exception(Error);
            }
        }

    }
}