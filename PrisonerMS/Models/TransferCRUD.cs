using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonerMS.Models
{
    public class TransferCRUD
    {
        public static List<Transfer> GetAllTransfers()
        {
            List<Transfer> TransfersList = new List<Transfer>();

            Transfer t1 = new Transfer();
            t1.TrnaferID = 1;
            t1.Type = "Prison";
            t1.TypeNumber = "64";
            t1.Description = "sjdbcjs";
            t1.Status = "Placed";
            t1.PrisonerID = 1;
            t1.PrisonerNo = "5555";
            t1.PrisonerName = "Tim Corey";
            t1.DateEntry = "11/11/2020";
            TransfersList.Add(t1);

            Transfer t2 = new Transfer();
            t2.TrnaferID = 1;
            t2.Type = "Hospital";
            t2.TypeNumber = "-";
            t1.Description = "sjdbcjs";
            t1.Status = "Placed";
            t2.PrisonerID = 1;
            t2.PrisonerNo = "5555";
            t2.PrisonerName = "Tim Corey";
            t2.DateEntry = "11/11/2020";
            TransfersList.Add(t2);

            Transfer t3 = new Transfer();
            t3.TrnaferID = 1;
            t3.Type = "Court";
            t3.TypeNumber = "-";
            t1.Description = "sjdbcjs";
            t1.Status = "Approved";
            t3.PrisonerID = 1;
            t3.PrisonerNo = "5555";
            t3.PrisonerName = "Tim Corey";
            t3.DateEntry = "11/11/2020";
            TransfersList.Add(t3);

            return TransfersList;
        }
    }
}