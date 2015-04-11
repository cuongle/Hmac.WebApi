using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hmac.Application
{
    public interface IValuesService
    {
        string[] GetValues();
        string GetValue(int valueId);
    }
}
