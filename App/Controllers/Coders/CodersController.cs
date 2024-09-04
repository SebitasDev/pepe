using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.App.Controllers
{
    public class CodersController : Controller
    {
        private readonly ICoderRepository _coderRepository;
        public string Error = "Server Error: The request has not been resolve";
        public CodersController(ICoderRepository coderRepository)
        {
            _coderRepository = coderRepository;
        }

        //endpoint
        [HttpGet]
        [Route("RiwiTalent/CoderList")]
        public async Task<IActionResult> Get()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(RiwiTalent.Utils.Exceptions.StatusError.CreateBadRequest());
            }

            try
            {
                var coders = await _coderRepository.GetCoders();
                return Ok(coders);
            }
            catch (Exception)
            {
                throw new Exception(Error);
            }
        }
    }
}
