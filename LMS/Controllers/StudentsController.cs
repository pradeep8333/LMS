using System.Web.Mvc;

namespace LMS.Controllers
{
    public class StudentsController : Controller
    {
        // GET: Student Dashboard
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult StudentChangePassword()
        {
            return View();
        }

        // GET: Profile
        public ActionResult ProfileDetails()
        {
            return View();
        }

        // GET: My Books
        public ActionResult MyBooks()
        {
            return View();
        }

        // GET: Search Books
        public ActionResult SearchBooks()
        {
            return View();
        }

        // GET: Fine & Payments
        public ActionResult FinesandPayments()
        {
            return View();
        }
    }
}
