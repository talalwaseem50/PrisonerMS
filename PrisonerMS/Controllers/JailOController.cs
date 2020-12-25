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
            return View(new Tuple<Prisoner, List<Punishment>>(new Prisoner(), PunishmentCRUD.GetAllPunishments()));
        }

        public ActionResult UserInfo()
        {
            return View();
        }

        public ActionResult EditPrisoner(int id)
        {
            Prisoner ptemp = PrisonerCRUD.GetPrisoner(1);
            return PartialView("_EditPrisoner", ptemp);
        }

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


        public ActionResult ComplaintStatus(int id)
        {
            return PartialView("_ComplaintStatus", id);
        }



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
            if (true)
                return Content("<script>alert('Visitor Deleted Successfully.');window.location.href=document.referrer;</script>");
            else
                return Content("<script>alert('Visitor could not be found.');window.location.href=document.referrer</script>");
        }

        public ActionResult RemoveTransfer(int id)
        {
            if (true)
                return Content("<script>alert('Transfer Deleted Successfully.');window.location.href=document.referrer;</script>");
            else
                return Content("<script>alert('Transfer could not be found.');window.location.href=document.referrer</script>");
        }
    }
}