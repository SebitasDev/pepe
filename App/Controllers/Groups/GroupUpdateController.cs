using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Services.Interface;
using RiwiTalent.Models;

namespace RiwiTalent.App.Controllers.Groups
{
    public class GroupUpdateController : Controller
    {
        private readonly IGroupCoderRepository _groupRepository;
        public string Error = "Server Error: The request has not been resolve";
        public GroupUpdateController(IGroupCoderRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        //Endpoint
        [HttpPut]
        [Route("RiwiTalent/UpdateGroups")]
        public async Task<IActionResult> UpdateGroups(GruopCoder gruopCoder)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(Utils.Exceptions.StatusError.CreateBadRequest());
            }
            
            try
            {
                await _groupRepository.Update(gruopCoder);
                return Ok("The Group has been updated the correct way");
            }
            catch (Exception ex)
            {
                throw new Exception(Error, ex);
            }

        }
    }
}