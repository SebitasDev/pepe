using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.App.Controllers
{
    public class CodersController : Controller
    {
        private readonly ICoderRepository _coderRepository;
        private readonly IGroupCoderRepository _groupCoderRepository;
        public CodersController(ICoderRepository coderRepository, IGroupCoderRepository groupCoderRepository)
        {
            _coderRepository = coderRepository;
            _groupCoderRepository = groupCoderRepository;
        }

        //get all coders
        [HttpGet]
        [Route("riwitalent/coders")]
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw;
            }
        }


        //Get all coders pagination
        [HttpGet]
        [Route("riwitalent/coders/page={page}")]
        public async Task<IActionResult> Get(int page = 1,int cantRegisters = 10)
        {
            /*The main idea of this method, is when the user list all coders can watch for pagination*/
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw;
            }
        }

        //Get coder by id
        [HttpGet]
        [Route("riwitalent/{id}/coder")]
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw;
            }
        }

        //Get Coder by name
        [HttpGet]
        [Route("riwitalent/{name}/coders")]
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw;
            }
        }
        
        //Get coders by group
        [HttpGet]
        [Route("riwitalent/group/{name}/coders")]
        public async Task<IActionResult> GetCodersByGroup(string name)
        {
            try
            {
                var groupExist = await _groupCoderRepository.GroupExistByname(name);
                if (!groupExist)
                {
                    return NotFound($"El grupo '{name}' no existe.");
                }

                var coder = await _coderRepository.GetCodersByGroup(name);
                if (coder == null || !coder.Any())
                {  
                    return NotFound($"No existe coders por el grupo '{name}'.");
                }
                return Ok(coder);
            }
            catch (Exception ex)
            {
                throw new ApplicationException($"Error al obtener los coders del grupo '{name}'", ex);
            }
        }
    }
}
