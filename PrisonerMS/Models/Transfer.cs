using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonerMS.Models
{
    public class Transfer
    {
        public int TransferID { get; set; }
        public string Type { get; set; }
        public string TypeNumber { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public string EntryDate { get; set; }
        public string EntryTime { get; set; }
        public Prisoner Prisoner { get; set; }
        public Prison Prison { get; set; }

    }
}