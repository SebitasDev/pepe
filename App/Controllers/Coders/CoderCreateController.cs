using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.App.Controllers.Coders
{
    public class CoderCreateController : Controller
    {
        private readonly ICoderRepository _coderRepository;
        private readonly IValidator<Coder> _coderValidator;
        public string Error = "Server Error: The request has not been resolve";
        public CoderCreateController(ICoderRepository coderRepository, IValidator<Coder> coderValidator)
        {
            _coderRepository = coderRepository;
            _coderValidator = coderValidator;
        }

        //Endpoint
        [HttpPost]
        [Route("RiwiTalent/CreateCoders")]
        public IActionResult Post([FromBody] Coder coder)
        {


            if(coder == null)
            {
                return BadRequest(Utils.Exceptions.StatusError.CreateBadRequest());
            }

            var ValidationResult = _coderValidator.Validate(coder);

            if(!ValidationResult.IsValid)
            {
                return BadRequest(ValidationResult.Errors);
            }

            try
            {
                _coderRepository.add(coder);
                return Ok("The coder has been created successfully");
            }
            catch (Exception)
            {
                throw new Exception(Error);
            }
        }

    }
}