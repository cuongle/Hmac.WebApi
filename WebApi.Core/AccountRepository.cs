using System;

namespace WebApi.Core
{
    public class AccountRepository: IAccountRepository
    {
        public string GetHashedPassword(string username)
        {
            return "password";
        }
    }
}
