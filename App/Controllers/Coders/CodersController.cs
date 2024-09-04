using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Services.Interface;

namespace MongoDb.App.Controllers
{
    public class CodersController : Controller
    {
        private readonly ICoderRepository _coderRepository;
        public CodersController(ICoderRepository coderRepository)
        {
            _coderRepository = coderRepository;
        }

        //endpoint
        [HttpGet]
        [Route("RiwiTalent/CoderList")]
        public async Task<IActionResult> Get()
        {
            return Ok(await _coderRepository.GetCoders());
        }
    }
}