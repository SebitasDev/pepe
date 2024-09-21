using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;
using RiwiTalent.Services.Interface;
using RiwiTalent.Utils.ExternalKey;

namespace RiwiTalent.App.Controllers.Groups
{
    public class GroupsController : Controller
    {
        private readonly IGroupCoderRepository _groupRepository;
        private readonly ExternalKeyUtils _service;
        public GroupsController(IGroupCoderRepository groupRepository, ExternalKeyUtils service)
        {
            _groupRepository = groupRepository;
            _service = service;
        }

        [HttpGet]
        [Route("riwitalent/groups")]
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

        //obtener el uuid y revertirlo
        [HttpPost]
        [Route("riwitalent/uuid")]
        public async Task<IActionResult> GetUUID([FromQuery] string id, [FromQuery] string key)
        {
            try
            {

                var objectId = ObjectId.Parse(id);
                var groupCoder = new GruopCoder { Id = objectId};

                await _groupRepository.SendToken(groupCoder, key);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw;
            }
        }
    }
}
