using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WebAPI.Hmac.Models
{
    public class Customer
    {
        public string Name { get; set; }

        // public string Address { get; set; }

        public Address Address { get; set; }

        public Phone[] Phones { get; set; }
    }
}