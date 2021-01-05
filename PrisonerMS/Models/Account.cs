using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonerMS.Models
{
    public class Account
    {
        public int UserID { get; set; }
        public string FullName { get; set; }
        public char Gender { get; set; }
        public string ContactNo { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string AccType { get; set; }
        public string DateJoined { get; set; }
        public Prison Prison { get; set; }
    }
}