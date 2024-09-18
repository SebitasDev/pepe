using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Services.Interface;
using RiwiTalent.Models;
using FluentValidation;
using MongoDB.Bson;
using RiwiTalent.Utils.ExternalKey;
using RiwiTalent.Models.Enums;

namespace RiwiTalent.App.Controllers.Groups
{
    public class GroupCreateController : Controller
    {
        private readonly IGroupCoderRepository _groupRepository;
        private readonly IValidator<GruopCoder> _groupValidator;
        private readonly ExternalKeyUtils _service;
        public string Error = "Server Error: The request has not been resolve";
        public GroupCreateController(IGroupCoderRepository groupRepository, IValidator<GruopCoder> groupValidator, ExternalKeyUtils service)
        {
            _groupRepository = groupRepository;
            _groupValidator = groupValidator;
            _service = service;
        }

        //endpoint
        [HttpPost]
        [Route("riwitalent/creategroups")]
        public IActionResult Post([FromBody] GruopCoder groupCoder)
        {
            //we create a new instance to can validate
            if(groupCoder == null)
            {
                return BadRequest("GroupCoderDto cannot be null.");
            } 

            var GroupValidations = _groupValidator.Validate(groupCoder);

            if(!GroupValidations.IsValid)
            {
                return BadRequest(GroupValidations.Errors);
            }

            try
            {
                ObjectId objectId = ObjectId.GenerateNewId();
                groupCoder.Id = objectId;
                Guid guid = _service.ObjectIdToUUID(objectId);


                //we define the path of url link
                string Link = $"http://riwitalent/external/{guid}";
                string tokenString = _service.GenerateTokenRandom();

                //define a new instance to add uuid into externalkeys -> url
                GruopCoder newGruopCoder = new GruopCoder
                {
                    Id = objectId,
                    Name = groupCoder.Name,
                    Description = groupCoder.Description,
                    Created_At = DateTime.UtcNow,
                    Coders = groupCoder.Coders,
                    ExternalKeys = new List<ExternalKey>
                    {
                        new ExternalKey
                        {
                            Url = Link,
                            Key = tokenString,
                            Status = Status.Active.ToString(),
                            Date_Creation = DateTime.UtcNow,
                            Date_Expiration = DateTime.UtcNow.AddDays(7)
                        }
                    }
                };
                _groupRepository.Add(newGruopCoder);

                return Ok("The Group has been created successfully");
            }
            catch (Exception ex)
            {
                throw new Exception(Error, ex);
            }
        }
    }
}