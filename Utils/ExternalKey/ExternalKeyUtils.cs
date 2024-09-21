using MongoDB.Bson;
using RiwiTalent.Models;
using RiwiTalent.Models.DTOs;
using RiwiTalent.Services.Interface;

namespace RiwiTalent.Utils.ExternalKey
{
    public class ExternalKeyUtils
    {

        public static Random random = new Random();


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

        //revertir UUID
        public string RevertObjectIdUUID(Guid guid)
        {

            string UUID = guid.ToString();

            List<string> ObjectIdUUID = new List<string>();


            foreach (var uuid in UUID)
            {
                ObjectIdUUID.Add(uuid.ToString());
            }

            string result = string.Join("", ObjectIdUUID).Replace("-", "");

            //we valdiate the lenght
            if(result.Length > 24)
            {
                result = result.Remove(result.Length - 8);
            }

            return result;
        }
    }
}