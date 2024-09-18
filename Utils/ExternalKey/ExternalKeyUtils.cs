using MongoDB.Bson;

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
    }
}