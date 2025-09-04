using System.Linq;
using System.Web.Mvc;
using LMS.Models;

namespace LMS.Controllers
{
    public class LMSController : Controller
    {
        private readonly LMSEntities _db = new LMSEntities();

        // ✅ Home Page
        public ActionResult Home()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Login");

            return View();
        }

        // ✅ Dashboard
        public ActionResult Dashboard()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Login");

            return View();
        }

        // ✅ Change Password
        [HttpGet]
        public ActionResult ChangePassword()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Login");

            return View();
        }

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

            return View();
        }

        // ✅ Add Book
        public ActionResult AddBook()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Login");

            return View();
        }

        // ✅ View Books
        public ActionResult ViewBooks()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Login");

            return View();
        }

        // ✅ View Issued Books
        public ActionResult ViewIssuedBooks()
        {
            if (Session["UserID"] == null)
                return RedirectToAction("Login", "Login");

            return View();
        }
    }
}
