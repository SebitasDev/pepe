using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Services.Interface;
using RiwiTalent.Models;
using FluentValidation;
using RiwiTalent.Models.DTOs;
using RiwiTalent.Validators;

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
        [Route("RiwiTalent/CreateGroups")]
        public IActionResult Post([FromBody] GruopCoder groupCoder)
        {
            GroupCoderDto groupCoderDto = new GroupCoderDto
            {
                Name = groupCoder.Name,
                Description = groupCoder.Description
            }; //we create a new instance to can validate

            var GroupValidations = _groupValidator.Validate(groupCoderDto);

            if(!GroupValidations.IsValid)
            {
                return BadRequest(GroupValidations.Errors);
            }

            try
            {
                _groupRepository.add(groupCoder);
                return Ok("The Group has been created successfully");
            }
            catch (Exception ex)
            {
                throw new Exception(Error, ex);
            }
        }
    }
}