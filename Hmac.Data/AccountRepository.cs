using Hmac.Core;

namespace Hmac.Data
{
    public class AccountRepository : IAccountRepository
    {
        public string GetHashedPassword(string username)
        {
            return "password";
        }
    }
}
