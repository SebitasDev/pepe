using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.App.Controllers.Groups
{
    public class GroupsController : Controller
    {
        private readonly IGroupCoderRepository _groupRepository;
        public string Error = "Server Error: The request has not been resolve";
        public GroupsController(IGroupCoderRepository groupRepository)
        {
            _groupRepository = groupRepository;
        }

        [HttpGet]
        [Route($"riwitalent/groups")]
        public async Task<IActionResult> Get()
        {
            var groupList = await _groupRepository.GetGroupCoders();
            return Ok(groupList);
        }
    }
}
