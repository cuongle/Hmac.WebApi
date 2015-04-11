using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmac.Core
{
    public interface IAccountRepository
    {
        string GetHashedPassword(string username);
    }
}
