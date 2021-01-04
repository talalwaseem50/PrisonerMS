using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonerMS.Models
{
    public class Medication
    {
        public int MedicationID { get; set; }
        public string Desc { get; set; }
        public string EntryDate { get; set; }
        public Medication MedicationR { get; set; }
    }
}