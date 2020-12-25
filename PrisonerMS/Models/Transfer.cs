using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonerMS.Models
{
    public class Transfer
    {
        public int TrnaferID { get; set; }
        public string Type { get; set; }
        public string TypeNumber { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int PrisonerID { get; set; }
        public string PrisonerNo { get; set; }
        public string PrisonerName { get; set; }
        public string DateEntry { get; set; }
    }
}