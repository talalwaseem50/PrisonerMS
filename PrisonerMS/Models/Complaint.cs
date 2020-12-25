using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonerMS.Models
{
    public class Complaint
    {
        public int ComplaintID { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public string DateRegister { get; set; }
        public string DateResolve { get; set; }
        public int PrisonerID { get; set; }
        public string PrisonerNo { get; set; }
        public string PrisonerName { get; set; }

    }
}