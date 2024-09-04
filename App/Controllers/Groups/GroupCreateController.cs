using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Services.Interface;
using RiwiTalent.Models;

namespace RiwiTalent.App.Controllers.Groups
{
    public class GroupCreateController : Controller
    {
        private readonly IGroupCoderRepository _groupRepository;
        public string Error = "Server Error: The request has not been resolve";
        public GroupCreateController(IGroupCoderRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        //endpoint
        [HttpPost]
        [Route("RiwiTalent/CreateGroups")]
        public IActionResult Post([FromBody] GruopCoder gruopCoder)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(Utils.Exceptions.StatusError.CreateBadRequest());
            }

            try
            {
                _groupRepository.add(gruopCoder);
                return Ok("The Group has been created successfully");
            }
            catch (Exception)
            {
                throw new Exception(Error);
            }
        }
    }
}