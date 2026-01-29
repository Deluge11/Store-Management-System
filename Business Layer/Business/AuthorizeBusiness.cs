using Data_Layer.Data;

namespace Business_Layer.Business
{
    public class AuthorizeBusiness 
    {
        public AuthorizeData AuthorizeData { get; }

        public AuthorizeBusiness(AuthorizeData authorizeData)
        {
            AuthorizeData = authorizeData;
        }


        public async Task<List<int>> GetPermissions(int userId)
        {
            return await AuthorizeData.GetPermissions(userId);
        }

    }
}
