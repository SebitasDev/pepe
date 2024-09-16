using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Services.Interface;
using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Cors;

namespace RiwiTalent.App.Controllers.Groups
{
    
    public class GroupUpdateController : Controller
    {
        private readonly IGroupCoderRepository _groupRepository;
        private readonly IMapper _mapper;
        public string Error = "Server Error: The request has not been resolve";
        public GroupUpdateController(IGroupCoderRepository groupRepository, IMapper mapper)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
        }

        //Endpoint
        [HttpPut]
        [Route("RiwiTalent/UpdateGroups")]
        [EnableCors("PolicyCors")]
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
                throw new Exception(Error, ex);
            }

        }
    }
}