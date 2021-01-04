using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PrisonerMS.Models;
using System.Web.Mvc;

namespace PrisonerMS.Controllers
{
    public class JailOController : Controller
    {
        // GET: JailO
        public ActionResult DashBoard()
        {
            return View();
        }

        public ActionResult Prisoners()
        {
            return View(PrisonerCRUD.GetAllPrisoners());
        }

        public ActionResult Complaints()
        {
            return View(ComplaintCRUD.GetAllComplaints());
        }

        public ActionResult Visitors()
        {
            return View(VisitorCRUD.GetAllVisitors());
        }

        public ActionResult Transfers()
        {
            return View(TransferCRUD.GetAllTransfers());
        }

        public ActionResult PrisonerDetails()
        {
            return View(new Tuple<Prisoner, List<Punishment>>(PrisonerCRUD.GetPrisoner(1), PunishmentCRUD.GetAllPunishments()));
        }

        public ActionResult UserInfo()
        {
            return View();
        }


        //Load Extend
        public ActionResult MedicalRecords(int id)
        {
            List<Medical> mrList = MedicalCRUD.GetAllPrisonerMedicals(1);
            List<Medication> mList = new List<Medication>();
            List<Allergy> aList = new List<Allergy>();

            foreach (Medical mr in mrList)
            {
                mList.AddRange(MedicationCRUD.GetAllMedicalMedications(mr.MedicalID));
                aList.AddRange(AllergyCRUD.GetAllMedicalAllergys(mr.MedicalID));
            }

            return PartialView("_MedicalRecords", new Tuple<List<Medical>, List<Medication>, List<Allergy>>(mrList, mList, aList));
        }

        //Detail
        public ActionResult DetailVisitor(int id)
        {
            return PartialView("_DetailVisitor", VisitorCRUD.GetVisitor(id));
        }


        //Edit
        public ActionResult EditPrisoner(int id)
        {
            Prisoner ptemp = PrisonerCRUD.GetPrisoner(1);
            return PartialView("_EditPrisoner", ptemp);
        }

        public ActionResult EditVisitor(int id)
        {
            Visitor visitor = VisitorCRUD.GetVisitor(id);
            return PartialView("_EditVisitor", visitor);
        }

        public ActionResult EditTransfer(int id)
        {
            return PartialView("_EditTransfer", TransferCRUD.GetTransfer(id));
        }

        public ActionResult EditMedical(int id)
        {
            return PartialView("_EditMedical", MedicalCRUD.GetMedical(id));
        }


        //Add
        public ActionResult AddComplaint(int id)
        {
            return PartialView("_AddComplaint", 1);
        }

        public ActionResult AddVisitor(int id)
        {
            return PartialView("_AddVisitor", 1);
        }

        public ActionResult AddTransfer(int id)
        {
            return PartialView("_AddTransfer", 1);
        }


        //Form
        [HttpPost]
        public ActionResult AddVisitorForm(FormCollection collection)
        {
            Visitor visitor = new Visitor();
            visitor.FirstName = collection["FirstName"];
            visitor.LastName = collection["LastName"];
            visitor.Gender = collection["Gender"];
            visitor.CNIC = collection["CNIC"];
            visitor.Address = collection["Address"];
            visitor.Relation = collection["Relation"];
            visitor.Prisoner = new Prisoner();
            visitor.Prisoner.PrisonerID = Int32.Parse(collection["PrisonerID"]);

            if (VisitorCRUD.InsertVisitor(visitor))
                return Content("<script>alert('Visitor Added Successfully.');window.location.href=document.referrer;</script>");
            else
                return Content("<script>alert('Visitor could not be Added');window.location.href=document.referrer</script>");
        }

        [HttpPost]
        public ActionResult EditVisitorForm(FormCollection collection)
        {
            Visitor visitor = new Visitor();
            visitor.FirstName = collection["FirstName"];
            visitor.LastName = collection["LastName"];
            visitor.Gender = collection["Gender"];
            visitor.CNIC = collection["CNIC"];
            visitor.Address = collection["Address"];
            visitor.Relation = collection["Relation"];
            visitor.VisitorRecordID = Int32.Parse(collection["VisitorID"]);

            if (VisitorCRUD.UpdateVisitor(visitor))
                return Content("<script>alert('Visiter Info Updated Successfully.');window.location.href=document.referrer;</script>");
            else
                return Content("<script>alert('Visitor Info could not be Updated');window.location.href=document.referrer</script>");
        }

        [HttpPost]
        public ActionResult AddTransferForm(FormCollection collection)
        {
            Transfer transfer = new Transfer();
            transfer.Type = collection["Type"];
            transfer.Description = collection["Desc"];
            transfer.PrisonerID = Int32.Parse(collection["PrisonerID"]);
            transfer.Status = "InProgress";

            if (transfer.Type.Equals("Prison"))
                transfer.TypeNumber = collection["TypeNumber"];
            else
                transfer.TypeNumber = "-";

            return null;
            //if (AccountCRUD.UpdateUser(myacc))
            //    return Content("<script>alert('Profile Edited Successfully.');window.location.href=document.referrer;</script>");
            //else
            //    return Content("<script>alert('Profile Could not be Updated');window.location.href=document.referrer</script>");
        }

        [HttpPost]
        public ActionResult EditTransferForm(FormCollection collection)
        {
            Transfer transfer = new Transfer();
            transfer.Description = collection["Desc"];
            transfer.TransferID = Int32.Parse(collection["TransferID"]);

            return null;
            //if (AccountCRUD.UpdateUser(myacc))
            //    return Content("<script>alert('Profile Edited Successfully.');window.location.href=document.referrer;</script>");
            //else
            //    return Content("<script>alert('Profile Could not be Updated');window.location.href=document.referrer</script>");
        }

        [HttpPost]
        public ActionResult StatusTransferForm(FormCollection collection)
        {
            string status = collection["TransferStatus"];

            return null;
            //if (AccountCRUD.UpdateUser(myacc))
            //    return Content("<script>alert('Profile Edited Successfully.');window.location.href=document.referrer;</script>");
            //else
            //    return Content("<script>alert('Profile Could not be Updated');window.location.href=document.referrer</script>");
        }

        [HttpPost]
        public ActionResult StatusTransferIncomingForm(FormCollection collection)
        {
            string status = collection["TransferStatus"];

            return null;
            //if (AccountCRUD.UpdateUser(myacc))
            //    return Content("<script>alert('Profile Edited Successfully.');window.location.href=document.referrer;</script>");
            //else
            //    return Content("<script>alert('Profile Could not be Updated');window.location.href=document.referrer</script>");
        }

        [HttpPost]
        public ActionResult AddMedicalForm(FormCollection collection)
        {
            Medical medical = new Medical();
            medical.Symptoms = collection["Symptoms"];
            medical.Diagnosis = collection["Diagnosis"];
            medical.Prisoner = new Prisoner();
            medical.Prisoner.PrisonerID = Int32.Parse(collection["PrisonerID"]);

            if (MedicalCRUD.InsertMedical(medical))
                return Content("<script>alert('Medical Added Successfully.');window.location.href=document.referrer;</script>");
            else
                return Content("<script>alert('Medical could not be Added');window.location.href=document.referrer</script>");
        }

        [HttpPost]
        public ActionResult EditMedicalForm(FormCollection collection)
        {
            Medical medical = new Medical();
            medical.Symptoms = collection["Symptoms"];
            medical.Diagnosis = collection["Diagnosis"];
            medical.Prisoner = new Prisoner();
            medical.MedicalID = Int32.Parse(collection["MedicalID"]);

            if (MedicalCRUD.UpdateMedical(medical))
                return Content("<script>alert('Medical Updated Successfully.');window.location.href=document.referrer;</script>");
            else
                return Content("<script>alert('Medical could not be Updated');window.location.href=document.referrer</script>");
        }



        //Status
        public ActionResult ComplaintStatus(int id)
        {
            return PartialView("_ComplaintStatus", id);
        }

        public ActionResult StatusTransfer(int id, int p, int c)
        {
            if (p == 1 & c == 0)
                return PartialView("_StatusTransfer", new Tuple<int, int>(id, 1));
            else if (p == 0 & c == 0)
                return PartialView("_StatusTransfer", new Tuple<int, int>(id, 2));
            else
                return PartialView("_StatusTransfer", new Tuple<int, int>(id, 3));
        }

        public ActionResult StatusTransferIncoming(int id)
        {
            return PartialView("_StatusTransferIncoming", id);
        }


        //Delete
        public ActionResult RemovePrisoner(int id)
        {
            if (true)
                return Content("<script>alert('Prisoner Deleted Successfully.');window.location.href=document.referrer;</script>");
            else
                return Content("<script>alert('Prisoner could not be found.');window.location.href=document.referrer</script>");
        }

        public ActionResult RemoveComplaint(int id)
        {
            if (true)
                return Content("<script>alert('Complaint Deleted Successfully.');window.location.href=document.referrer;</script>");
            else
                return Content("<script>alert('Complaint could not be found.');window.location.href=document.referrer</script>");
        }

        public ActionResult RemoveVisitor(int id)
        {
            if (VisitorCRUD.DeleteVisitor(id))
                return Content("<script>alert('Visitor Deleted Successfully');window.location.href=document.referrer;</script>");
            else
                return Content("<script>alert('Visitor could not be Deleted');window.location.href=document.referrer</script>");
        }

        public ActionResult RemoveTransfer(int id)
        {
            if (true)
                return Content("<script>alert('Transfer Deleted Successfully.');window.location.href=document.referrer;</script>");
            else
                return Content("<script>alert('Transfer could not be found.');window.location.href=document.referrer</script>");
        }

        public ActionResult RemoveMedical(int id)
        {
            if (MedicalCRUD.DeleteMedical(id))
                return Content("<script>alert('Medical Record Deleted Successfully.');window.location.href=document.referrer;</script>");
            else
                return Content("<script>alert('Medical Record could not be Deleted.');window.location.href=document.referrer</script>");
        }
    }
}