using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Services.Interface;
using RiwiTalent.Models;
using FluentValidation;
using RiwiTalent.Models.DTOs;

namespace RiwiTalent.App.Controllers.Groups
{
    public class GroupCreateController : Controller
    {
        private readonly IValidator<GruopCoder> _groupValidator;
        private readonly IGroupCoderRepository _groupRepository;
        public string Error = "Server Error: The request has not been resolve";
        public GroupCreateController(IGroupCoderRepository groupRepository, IValidator<GruopCoder> groupValidator)
        {
            _groupRepository = groupRepository;
            _groupValidator = groupValidator;
        }

        //endpoint
        [HttpPost]
        [Route("riwitalent/creategroups")]
        // public IActionResult Post([FromBody] GruopCoder groupCoder, CoderDto coderDto)
        public IActionResult Post([FromBody] GroupDto groupDto)
        {
            //we create a new instance to can validate
            if(groupDto == null)
            {
                return BadRequest("GroupCoderDto cannot be null.");
            } 

            /* var GroupValidations = _groupValidator.Validate(groupDto); */

            /* if(!GroupValidations.IsValid)
            {
                return BadRequest(GroupValidations.Errors);
            } */

            try
            {
                _groupRepository.Add(groupDto);

                return Ok("The Group has been created successfully");
            }
            catch (Exception ex)
            {
                throw new Exception(Error, ex);
            }
        }
    }
}