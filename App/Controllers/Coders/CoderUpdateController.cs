using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;
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
        public async Task<IActionResult> UpdateCoder(CoderDto coderDto)
        {
            if(coderDto == null)
            {
                return BadRequest(Utils.Exceptions.StatusError.CreateBadRequest());
            }

            try
            {
                await _coderRepository.Update(coderDto);
                return Ok("The coder has been updated the correct way");
            }
            catch (Exception)
            {
                throw new Exception(Error);
            }
        }
    }
}