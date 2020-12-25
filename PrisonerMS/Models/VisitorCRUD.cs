using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonerMS.Models
{
    public class VisitorCRUD
    {
        public static List<Visitor> GetAllVisitors()
        {
            List<Visitor> VisitorsList = new List<Visitor>();

            Visitor v1 = new Visitor();
            v1.VisitorRecordID = 1;
            v1.FullName = "Muhammad Sarwar";
            v1.CNIC = "35202333333";
            v1.Address = "fcsncjnskjcnksdnk";
            v1.PrisonerID = 1;
            v1.PrisonerNo = "5555";
            v1.PrisonerName = "Tim Corey";
            VisitorsList.Add(v1);

            v1 = new Visitor();
            v1.VisitorRecordID = 1;
            v1.FullName = "Muhammad Sarwar";
            v1.CNIC = "35202333333";
            v1.Address = "fcsncjnskjcnksdnk";
            v1.PrisonerID = 1;
            v1.PrisonerNo = "5555";
            v1.PrisonerName = "Tim Corey";
            VisitorsList.Add(v1);

            v1 = new Visitor();
            v1.VisitorRecordID = 1;
            v1.FullName = "Muhammad Sarwar";
            v1.CNIC = "35202333333";
            v1.Address = "fcsncjnskjcnksdnk";
            v1.PrisonerID = 1;
            v1.PrisonerNo = "5555";
            v1.PrisonerName = "Tim Corey";
            VisitorsList.Add(v1);


            return VisitorsList;
        }
    }
}