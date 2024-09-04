using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDb.Models;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.App.Controllers.Coders
{
    public class CoderCreateController : Controller
    {
        private readonly ICoderRepository _coderRepository;
        public CoderCreateController(ICoderRepository coderRepository)
        {
            _coderRepository = coderRepository;
        }

        //Endpoint
        [HttpPost]
        [Route("RiwiTalent/CreateCoders")]
        public IActionResult Post([FromBody] Coder coder)
        {

            if(!ModelState.IsValid)
            {
                return BadRequest();
            }

            try
            {
                _coderRepository.add(coder);
                return Ok("The coder has been created successfully");
            }
            catch (Exception ex)
            {
                
                throw new Exception("The coder has not been created", ex);
            }
        }
    }
}