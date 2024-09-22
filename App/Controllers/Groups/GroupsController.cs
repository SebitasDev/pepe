using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
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

                var groupCoder = new GruopCoder { UUID = id};


                await _groupRepository.SendToken(groupCoder, key);
                return Ok("you've access");
            }
            catch(Exception ex)
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
