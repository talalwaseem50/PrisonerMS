using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PrisonerMS.Models
{
    public class VisitorCRUD
    {

        public static Visitor GetVisitor(int id)
        {
            Visitor v1 = new Visitor();
            v1.VisitorRecordID = 1;
            v1.FirstName = "Muhammad";
            v1.LastName = "Sarwar";
            v1.Gender = "M";
            v1.CNIC = "35202333333";
            v1.Address = "fcsncjnskjcnksdnk";
            v1.Relation = "Brother";
            v1.VisitDate = "11/11/2020";
            v1.Prisoner = new Prisoner();
            v1.Prisoner.PrisonerID = 1;
            v1.Prisoner.PrisonerNo = "5555";
            v1.Prisoner.FullName = "Tim Corey";

            return v1;
        }

        public static List<Visitor> GetAllVisitors()
        {
            List<Visitor> VisitorsList = new List<Visitor>();

            Visitor v1 = new Visitor();
            v1.VisitorRecordID = 1;
            v1.FirstName = "Muhammad";
            v1.LastName = "Sarwar";
            v1.Gender = "M";
            v1.CNIC = "35202333333";
            v1.Address = "fcsncjnskjcnksdnk";
            v1.Relation = "Brother";
            v1.VisitDate = "11/11/2020";
            v1.Prisoner = new Prisoner();
            v1.Prisoner.PrisonerID = 1;
            v1.Prisoner.PrisonerNo = "5555";
            v1.Prisoner.FullName = "Tim Corey";
            VisitorsList.Add(v1);

            return VisitorsList;
        }
    }
}