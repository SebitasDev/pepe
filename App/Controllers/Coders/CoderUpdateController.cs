using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Models.DTOs;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.App.Controllers.Coders
{
    public class CoderUpdateController : Controller
    {
        private readonly ICoderRepository _coderRepository;
        public CoderUpdateController(ICoderRepository coderRepository)
        {
            _coderRepository = coderRepository;
        }

        //Endpoint
        [HttpPut]
        [Route("riwitalent/updatecoder")]
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
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw;
            }
        }

        [HttpPut]
        [Route("riwitalent/addcodersgroup")]
        public async Task<IActionResult> AddCodersToGroup(CoderGroupDto coderGroup)
        {
            try
            {
                await _coderRepository.UpdateCodersGroup(coderGroup);
                return Ok("List of coders sucessfully added to group");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw;
            }
        }

        [HttpPut]
        [Route("riwitalent/updatecodersselected")]
        public async Task<IActionResult> AddSelectedCoders(CoderGroupDto coderGroup)
        {
            try
            {
                await _coderRepository.UpdateCodersSelected(coderGroup);
                return Ok("List of coders sucessfully selected by company");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw;
            }
        }
        
    }
}