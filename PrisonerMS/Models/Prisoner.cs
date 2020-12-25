using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonerMS.Models
{
    public class Prisoner
    {
        public int PrisonerID { get; set; }
        public string PrisonerNo { get; set; }
        public string FullName { get; set; }
        public char Gender { get; set; }
        public string FatherName { get; set; }
        public string CNIC { get; set; }
        public string Address { get; set; }
        public string DateBirth { get; set; }
        public string VisitingDay { get; set; }
        public string VisitingTime { get; set; }
        public string DateEntry { get; set; }
    }
}