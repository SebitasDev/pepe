using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Models.DTOs;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.App.Controllers.Groups
{
    public class GroupsController : Controller
    {
        private readonly IGroupCoderRepository _groupRepository;
        public GroupsController(IGroupCoderRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [HttpGet]
        [Route($"riwitalent/groups")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var groupList = await _groupRepository.GetGroupCoders();

                if(groupList is null)
                {
                    return NotFound(Utils.Exceptions.StatusError.CreateNotFound());
                }

                return Ok(groupList);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw;
            }
        }

        [HttpGet]
        [Route("riwitalent/groupdetails/{id}")]
        public async Task<IActionResult> GetGroupInfoById(string id)
        {
            try
            {
                GroupInfoDto groupInfo = await _groupRepository.GetGroupInfoById(id);

                if(groupInfo is null)
                {
                    return NotFound(Utils.Exceptions.StatusError.CreateNotFound());
                }

                return Ok(groupInfo);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw;
            }
        }
    }
}
