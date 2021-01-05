using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonerMS.Models
{
    public class Medical
    {
        public int MedicalID { get; set; }
        public string Symptoms { get; set; }
        public string Diagnosis { get; set; }
        public string EntryDate { get; set; }
        public string EntryTime { get; set; }
        public Prisoner Prisoner { get; set; }
    }
}