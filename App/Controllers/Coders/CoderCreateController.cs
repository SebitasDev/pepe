using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Models;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.App.Controllers.Coders
{
    public class CoderCreateController : Controller
    {
        private readonly ICoderRepository _coderRepository;
        private readonly IValidator<Coder> _coderValidator;
        public CoderCreateController(ICoderRepository coderRepository, IValidator<Coder> coderValidator)
        {
            _coderRepository = coderRepository;
            _coderValidator = coderValidator;
        }

        //Endpoint
        [HttpPost]
        [Route("riwitalent/createcoders")]
        public IActionResult Post([FromBody] Coder coder)
        {


            if(coder == null)
            {
                return BadRequest(Utils.Exceptions.StatusError.CreateBadRequest());
            }


            //validations with FluentValidation
            var ValidationResult = _coderValidator.Validate(coder);

            if(!ValidationResult.IsValid)
            {
                return BadRequest(ValidationResult.Errors);
            }

            try
            {
                _coderRepository.Add(coder);
                return Ok("The coder has been created successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw;
            }
        }

    }
}