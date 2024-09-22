using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.App.Controllers
{
    public class CoderStatusHistoryController : Controller
    {
        private readonly ICoderStatusHistoryRepository _coderStatusHistoryRepository;
        public CoderStatusHistoryController(ICoderStatusHistoryRepository coderStatusHistoryRepository)
        {
            _coderStatusHistoryRepository = coderStatusHistoryRepository;
        }

        //get all coders
        [HttpGet]
        [Route("riwitalent/coderstatushistory")]
        public async Task<IActionResult> Get()
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(RiwiTalent.Utils.Exceptions.StatusError.CreateBadRequest());
            }

            try
            {
                var coders = await _coderStatusHistoryRepository.GetCodersStatus();
                return Ok(coders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("riwitalent/groupCoders/{id}")]
        public async Task<IActionResult> GetByGroupId(string id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(RiwiTalent.Utils.Exceptions.StatusError.CreateBadRequest());
            }

            try
            {
                var coders = await _coderStatusHistoryRepository.GetCompanyGroupedCoders(id);
                return Ok(coders);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw;
            }
        }


    }
}