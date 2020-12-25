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
            List<Transfer> AllTransfers = null;//RequestCRUD.GetRequest((int)Session["UserID"]);

            AllComplaints.RemoveAll(item => item.Status == "Resolved");
            //AllRequests.RemoveAll(item => item.RequestStatus == "Resolved"); //only keep unresolved requests

            return View(new Tuple<List<Complaint>, List<Transfer>>(AllComplaints, AllTransfers));
        }

        public ActionResult Complaints()
        {
            return View(ComplaintCRUD.GetAllComplaints());
        }

        public ActionResult Transfers()
        {
            return View(TransferCRUD.GetAllTransfers());
        }

        public ActionResult PrisonerDetails()
        {
            return View(new Tuple<Prisoner, List<Punishment>>(new Prisoner(), PunishmentCRUD.GetAllPunishments()));
        }

        public ActionResult UserInfo()
        {
            return View();
        }

        public ActionResult ComplaintStatus(int id)
        {
            return PartialView("_ComplaintStatus", id);
        }
    }
}