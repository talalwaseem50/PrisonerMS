using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonerMS.Models
{
    public class Visitor
    {
        public int VisitorRecordID { get; set; }
        public string FullName { get; set; }
        public string CNIC { get; set; }
        public string Address { get; set; }
        public int PrisonerID { get; set; }
        public string PrisonerNo { get; set; }
        public string PrisonerName { get; set; }
    }
}