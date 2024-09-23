using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Services.Interface;
using RiwiTalent.Models.DTOs;
using RiwiTalent.Models;
using AutoMapper;

namespace RiwiTalent.App.Controllers.Groups
{

    public class GroupUpdateController : Controller
    {
        private readonly IGroupCoderRepository _groupRepository;
        private readonly IMapper _mapper;
        public GroupUpdateController(IGroupCoderRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        //Endpoint
        [HttpPut]
        [Route("riwitalent/updategroups")]
        public async Task<IActionResult> UpdateGroups(GroupCoderDto groupCoderDto)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(Utils.Exceptions.StatusError.CreateBadRequest());
            }
            
            try
            {
                await _groupRepository.Update(groupCoderDto);
                return Ok("The Group has been updated the correct way");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw;
            }

        }
    }
}