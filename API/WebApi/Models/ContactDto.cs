using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class ContactDto
    {
        public int ContactID { get; set; }
        public string ContactName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public string Status { get; set; }
    }
}
