using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonerMS.Models
{
    public class Visitor
    {
        public int VisitorRecordID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string CNIC { get; set; }
        public string Address { get; set; }
        public string Relation { get; set; }
        public string VisitDate { get; set; }
        public string VisitTime { get; set; }
        public Prisoner Prisoner { get; set; }
        public Prison Prison { get; set; }
    }
}