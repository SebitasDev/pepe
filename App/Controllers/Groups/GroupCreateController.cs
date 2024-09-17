using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Services.Interface;
using RiwiTalent.Models;
using FluentValidation;
using RiwiTalent.Models.DTOs;

namespace RiwiTalent.App.Controllers.Groups
{
    public class GroupCreateController : Controller
    {
        private readonly IGroupCoderRepository _groupRepository;
        private readonly IValidator<GroupCoderDto> _groupValidator;
        public string Error = "Server Error: The request has not been resolve";
        public GroupCreateController(IGroupCoderRepository groupRepository, IValidator<GroupCoderDto> groupValidator)
        {
            _groupRepository = groupRepository;
            _groupValidator = groupValidator;
        }

        //endpoint
        [HttpPost]
        [Route("riwitalent/creategroups")]
        public IActionResult Post([FromBody] GruopCoder groupCoder)
        {
            //we create a new instance to can validate
            GroupCoderDto groupCoderDto = new GroupCoderDto
            {
                Name = groupCoder.Name,
                Description = groupCoder.Description
            }; 

            var GroupValidations = _groupValidator.Validate(groupCoderDto);

            if(!GroupValidations.IsValid)
            {
                return BadRequest(GroupValidations.Errors);
            }

            try
            {
                _groupRepository.Add(groupCoder);
                return Ok("The Group has been created successfully");
            }
            catch (Exception ex)
            {
                throw new Exception(Error, ex);
            }
        }
    }
}