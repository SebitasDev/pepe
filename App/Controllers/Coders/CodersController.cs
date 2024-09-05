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

        //Get all coders
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

        //Get coder by id
        [HttpGet]
        [Route("RiwiTalent/{id}/Coder")]
        public async Task<IActionResult> GetCoderById(string id)
        {
            try
            {
                var coder = await _coderRepository.GetCoderId(id);

                if (coder is null)
                {
                    return NotFound(new { message = $"Coder con ID {id} no fue encontrado." });
                }

                return Ok(coder);
            }
            catch (Exception)
            {
                
                throw new Exception(Error);
            }
        }

        //Get Coder by name
        [HttpGet]
        [Route("RiwiTalent/{name}/Coders")]
        public async Task<IActionResult> GetCoderByName(string name)
        {
            try
            {
                var coder = await _coderRepository.GetCoderName(name);

                if (coder is null)
                {
                    return NotFound( new{ message = $"Coder con nombre {name} no fue encontrado."});
                }

                return Ok(coder);
            }
            catch (Exception)
            {
                
                throw new Exception(Error);
            }
        }
    }
}
