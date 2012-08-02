using System;

namespace WebApi.Core
{
    public class AccountRepository: IAccountRepository
    {
        public string GetEncryptedPassword(string username)
        {
            return "password";
        }
    }
}
