

namespace SharedDTO
{
    public class SharedEnums
    {
        public enum Roles
        {
            Seller = 1,
            Buyer = 2
        }

        public enum ApiResponseStatus
        {
            Success = 200,
            Created = 201,
            BadRequest = 400,
            NotFound = 404
        }
    }
}
