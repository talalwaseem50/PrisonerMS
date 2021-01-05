using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonerMS.Models
{
    public class Allergy
    {
        public int AllergyID { get; set; }
        public string Desc { get; set; }
        public string EntryDate { get; set; }
        public string EntryTime { get; set; }
        public Medical Medical { get; set; }
    }
}