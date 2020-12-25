using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonerMS.Models
{
    public class PunishmentCRUD
    {
        public static List<Punishment> GetAllPunishments()
        {
            List<Punishment> PunishmentsList = new List<Punishment>();

            Punishment p2 = new Punishment();
            p2.PunishmentID = 2;
            p2.Type = "Murder";
            p2.DateStart = "11/11/2020";
            p2.DateEnd = "-";
            p2.DateExecution = "13/12/2020";
            p2.Notes = "cskdmcksmdkcsd";
            p2.PrisonerID = 1;
            p2.PrisonID = 2;
            p2.PrisonNumber = "65";
            PunishmentsList.Add(p2);

            Punishment p1 = new Punishment();
            p1.PunishmentID = 1;
            p1.Type = "Aggravated Assault";
            p1.DateStart = "11/11/2019";
            p1.DateEnd = "13/02/2020";
            p1.DateExecution = "-";
            p1.Notes = "cskdmcksmdkcsd";
            p1.PrisonerID = 1;
            p1.PrisonID = 1;
            p1.PrisonNumber = "64";
            PunishmentsList.Add(p1);

            return PunishmentsList;
        }
    }
}