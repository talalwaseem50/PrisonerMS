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

        public ActionResult PrisonerDetails()
        {
            return View(new Tuple<Prisoner, List<Punishment>>(new Prisoner(), PunishmentCRUD.GetAllPunishments()));
        }

        public ActionResult UserInfo()
        {
            return View();
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


        //Form
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
    
    }
}