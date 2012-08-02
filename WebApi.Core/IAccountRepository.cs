using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi.Core
{
    public interface IAccountRepository
    {
        string GetEncryptedPassword(string username);
    }
}
