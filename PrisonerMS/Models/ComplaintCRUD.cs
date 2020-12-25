using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonerMS.Models
{
    public class ComplaintCRUD
    {

        public static List<Complaint> GetAllComplaints()
        {
            List<Complaint> ComplaintsList = new List<Complaint>();

            Complaint c1 = new Complaint();
            c1.ComplaintID = 1;
            c1.Status = "Placed";
            c1.Description = "Food Problem";
            c1.DateRegister = "11/11/2020";
            c1.DateResolve = "-";
            c1.PrisonerID = 1;
            c1.PrisonerNo = "5555";
            c1.PrisonerName = "Tim Corey";
            ComplaintsList.Add(c1);

            Complaint c2 = new Complaint();
            c2.ComplaintID = 2;
            c2.Status = "In Progress";
            c2.Description = "Food Problem";
            c2.DateRegister = "01/11/2020";
            c2.DateResolve = "-";
            c2.PrisonerID = 2;
            c2.PrisonerNo = "6666";
            c2.PrisonerName = "Jack Harris";
            ComplaintsList.Add(c2);

            Complaint c3 = new Complaint();
            c3.ComplaintID = 3;
            c3.Status = "Resolved";
            c3.Description = "Food Problem";
            c3.DateRegister = "29/10/2020";
            c3.DateResolve = "12/12/2020";
            c3.PrisonerID = 1;
            c3.PrisonerNo = "5555";
            c3.PrisonerName = "Tim Corey";
            ComplaintsList.Add(c3);


            return ComplaintsList;
        }





    }
}