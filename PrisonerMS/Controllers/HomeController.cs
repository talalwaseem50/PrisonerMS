using PrisonerMS.Models;
using System.Web.Mvc;

namespace PrisonerMS.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Authenticate(string uname, string password)
        {
            Account UserAcc = AccountCRUD.UserLogin(uname, password);

            if (UserAcc != null)
            {
                Session["UserID"] = UserAcc.UserID;
                Session["FullName"] = UserAcc.FullName;
                Session["UserType"] = UserAcc.AccType;
                Session["PrisonID"] = UserAcc.Prison.PrisonID;

                return RedirectToAction("DashBoard", UserAcc.AccType == "J" ? "Jailer" : "JailO");
            }
            else
                return Content("<script>alert('Incorrect Email or Password.');window.location = 'Login';</script>");
        }

        public ActionResult Logout()
        {
            Session["UserID"] = null;
            Session["FullName"] = null;
            Session["UserType"] = null;
            Session["PrisonID"] = null;

            return RedirectToAction("Index", "Home");
        }

        public ActionResult ErrorImplementation()
        {
            return View();
        }
    }
}