using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MongoDB.Bson;
using RiwiTalent.Models;
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

        //get all coders
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


        //Get all coders pagination
        [HttpGet]
        [Route("RiwiTalent/CoderList/page={page}")]
        public async Task<IActionResult> Get(int page = 1,int cantRegisters = 10)
        {

            try
            {
                var coderPagination = await _coderRepository.GetCodersPagination(page, cantRegisters);

                if(coderPagination == null)
                {
                    return BadRequest(RiwiTalent.Utils.Exceptions.StatusError.CreateBadRequest());
                }
                return Ok(new 
                {
                    Page = page,
                    Registers = coderPagination.Count(),
                    Coders = coderPagination,
                    TotalPages = coderPagination.TotalPages,
                    PageBefore = coderPagination.PageBefore,
                    PageAfter = coderPagination.PageAfter
                });
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

        //Get coder by stack tecnical
        [HttpGet]
        [Route("RiwiTalent/skill/coder")]
        public async Task<IActionResult> GetCodersByStack([FromQuery] List<string> stack)
        {
            try
            {
                var coders = await _coderRepository.GetCodersByStack(stack);
                if (coders is null || !coders.Any())
                {
                    return NotFound("No hay coder con esos lenguajes.");
                }

                return Ok(coders);
            }
            catch (Exception)
            {
                
                throw new Exception(Error);
            }
            
        }

        //Get coder by language level in english
        [HttpGet]
        [Route("RiwiTalent/coder/{language}/level")]
        public async Task<IActionResult> GetCodersByLanguage([FromQuery] string level, string language = "English")
        {
            try
            {
                var coders = await _coderRepository.GetCodersBylanguage(level);
                if (coders is null || !coders.Any())
                {
                    return NotFound("No hay coder con ese nivel de idioma.");
                }
                return Ok(coders);
            }
            catch (Exception)
            {
                
                throw new Exception(Error);
            }
        }
    }
}
