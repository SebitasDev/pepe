using Microsoft.AspNetCore.Mvc;
using RiwiTalent.Services.Interface;
using RiwiTalent.Models;
using FluentValidation;
using RiwiTalent.Models.DTOs;
using MongoDB.Bson;

namespace RiwiTalent.App.Controllers.Groups
{
    public class GroupCreateController : Controller
    {
        private readonly IGroupCoderRepository _groupRepository;
        private readonly IValidator<GruopCoder> _groupValidator;
        public static Random random = new Random();
        public string Error = "Server Error: The request has not been resolve";
        public GroupCreateController(IGroupCoderRepository groupRepository, IValidator<GruopCoder> groupValidator)
        {
            _groupRepository = groupRepository;
            _groupValidator = groupValidator;
        }

        //convert objectId at UUID
        public Guid ObjectIdToUUID(ObjectId objectId)
        {
            byte[] ObjectBytes = objectId.ToByteArray();

            byte[] UUIDBytes = new byte[16];

            Array.Copy(ObjectBytes, 0, UUIDBytes, 0, ObjectBytes.Length);

            for(int i = 12; i < 16; i++)
            {
                UUIDBytes[i] = 0;
            }

            return new Guid(UUIDBytes);
        }

        //Generate token
        public string GenerateTokenRandom()
        {
            //token
            List<int> token = new List<int> {};

            for(int i = 0; i < 4; i++)
            {
                int randomNumberInRange = random.Next(0, 10);

                //Add randomNumberInRange in token
                token.Add(randomNumberInRange);
                
            }

            return string.Join("", token);

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

                Guid guid = ObjectIdToUUID(objectId);


                //we define the path of url link
                string Link = $"https://riwitalent.com/groups/{guid}";
                string tokenString = GenerateTokenRandom();

                


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