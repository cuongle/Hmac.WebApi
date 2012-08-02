using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebApi.Core
{
    public class ValueService : IValueService
    {
        public string[] GetValues()
        {
            return new[] {"value1", "value2"};
        }

        public string GetValue(int valueId)
        {
            return "value";
        }
    }
}
