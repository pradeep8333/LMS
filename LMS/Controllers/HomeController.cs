using System;
using System.Linq;
using System.Web.Mvc;
using LMS.Models;

namespace LMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly LMSEntities _db = new LMSEntities();

        // ✅ Home Page
        public ActionResult HomePage()
        {
            return View(); // Views/Home/HomePage.cshtml
        }

        // ✅ Dashboard
        public ActionResult Dashboard()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Login");

            return View(); // Views/Home/Dashboard.cshtml
        }

        // ✅ Change Password (GET)
        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Login");

            return View(); // Views/Home/ChangePassword.cshtml
        }

        // ✅ Change Password (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(string NewPassword, string ConfirmPassword)
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Login");

            if (string.IsNullOrEmpty(NewPassword) || NewPassword.Length < 8 || NewPassword.Length > 15)
            {
                TempData["Error"] = "Password must be between 8 and 15 characters.";
                return View();
            }

            if (NewPassword != ConfirmPassword)
            {
                TempData["Error"] = "Passwords do not match.";
                return View();
            }

            var userId = Session["UserID"].ToString();
            var user = _db.Logins.FirstOrDefault(u => u.UserID == userId);

            if (user != null)
            {
                user.PasswordHash = NewPassword; // ⚠ Hash in real apps
                _db.SaveChanges();

                TempData["Success"] = "Password changed successfully.";
                return RedirectToAction("Dashboard");
            }

            TempData["Error"] = "Error updating password.";
            return View();
        }

        // ✅ Profile Details
        public ActionResult ProfileDetails()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Login");

            return View(); // Views/Home/ProfileDetails.cshtml
        }

        // ✅ Add Book
        public ActionResult AddBook()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Login");

            return View(); // Views/Home/AddBook.cshtml
        }

        // ✅ View Books
        public ActionResult ViewBooks()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Login");

            return View(); // Views/Home/ViewBooks.cshtml
        }

        // ✅ View Issued Books
        public ActionResult IssuedBooks()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Login");

            return View(); // Views/Home/ViewIssuedBooks.cshtml
        }

        public ActionResult ViewaIssuedBooks()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Login");

            return View(); // Views/Home/ViewaAllIssuedBooks.cshtml
        }
        // ✅ Manage Fines → Add Fine
        public ActionResult AddFines()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Login");

            return View(); // Views/Home/AddFine.cshtml
        }

        // ✅ Manage Fines → View Fines
        public ActionResult ViewFines()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Login");

            return View(); // Views/Home/ViewFines.cshtml
        }

        // ✅ Static Pages
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View(); // Views/Home/About.cshtml
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View(); // Views/Home/Contact.cshtml
        }
    }
}
