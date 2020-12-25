using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonerMS.Models
{
    public class Punishment
    {
        public int PunishmentID { get; set; }
        public string Type { get; set; }
        public string DateStart { get; set; }
        public string DateEnd { get; set; }
        public string DateExecution { get; set; }
        public string Notes { get; set; }
        public int PrisonID { get; set; }
        public string PrisonNumber { get; set; }
        public int PrisonerID { get; set; }
    }
}