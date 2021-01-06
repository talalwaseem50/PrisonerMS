using PrisonerMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PrisonerMS.Controllers
{
    public class JailerController : Controller
    {
        // GET: Jailer
        public ActionResult DashBoard()
        {
            List<Complaint> AllComplaints = ComplaintCRUD.GetAllComplaints();
            AllComplaints.RemoveAll(item => item.Status == "Resolved");

            return View(new Tuple<List<Complaint>, List<Transfer>>(AllComplaints, TransferCRUD.GetAdminTransfers((int)Session["PrisonID"])));
        }

        public ActionResult Complaints()
        {
            return View(ComplaintCRUD.GetAllComplaints());
        }

        public ActionResult Transfers()
        {
            return View(TransferCRUD.GetAllPrisonTransfers((int)Session["PrisonID"]));
        }

        public ActionResult PrisonerDetails(int id)
        {
            return View(new Tuple<Prisoner, List<Punishment>>(PrisonerCRUD.GetPrisoner(id), PunishmentCRUD.GetAllPunishments()));
        }

        public ActionResult UserInfo()
        {
            return View();
        }


        //Load Extend
        public ActionResult MedicalRecords(int id)
        {
            List<Medical> mrList = MedicalCRUD.GetAllPrisonerMedicals(id);
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
        public ActionResult DetailTransfer(int id)
        {
            return PartialView("_DetailTransfer", TransferCRUD.GetTransfer(id));
        }

        public ActionResult DetailMedical(int id)
        {
            Medical m = MedicalCRUD.GetMedical(id);
            List<Medication> mList = MedicationCRUD.GetAllMedicalMedications(id);
            List<Allergy> aList = AllergyCRUD.GetAllMedicalAllergys(id);

            return PartialView("_DetailMedical", new Tuple<Medical, List<Medication>, List<Allergy>>(m, mList, aList));
        }


        //Form
        [HttpPost]
        public ActionResult StatusTransferForm(FormCollection collection)
        {
            Transfer transfer = new Transfer();
            transfer.Status = collection["TransferStatus"];
            transfer.TransferID = Int32.Parse(collection["TransferID"]);

            if (TransferCRUD.UpdateStatusTransfer(transfer))
                return Content("<script>alert('Transfer Status Updated Successfully.');window.location.href=document.referrer;</script>");
            else
                return Content("<script>alert('Transfer Status could not be Updated');window.location.href=document.referrer</script>");
        }


        //Status
        public ActionResult ComplaintStatus(int id)
        {
            return PartialView("_ComplaintStatus", id);
        }

        public ActionResult StatusTransfer(int id, int p)
        {
            return PartialView("_StatusTransfer", new Tuple<int, int>(id, p));
        }


    }
}