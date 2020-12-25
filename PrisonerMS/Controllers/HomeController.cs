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

                return RedirectToAction("DashBoard", UserAcc.AccType == "J" ? "Jailer" : "JailO");
            }
            else
                return Content("<script>alert('Incorrect Email or Password.');window.location = 'Login';</script>");
        }

        public ActionResult Logout()
        {
            Session["UserID"] = null;
            Session["UserName"] = null;
            Session["Priviledges"] = null;
            Session["OrderItems"] = null;

            return RedirectToAction("Index", "Home");
        }
    }
}