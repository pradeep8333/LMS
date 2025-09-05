using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using LMS.Models;

namespace LMS.Controllers
{
    public class LoginController : Controller
    {
        private readonly LMSEntities _db = new LMSEntities();

        // GET: Login
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model, string CaptchaInput)
        {
            // Validate CAPTCHA
            string captchaStored = Session["Captcha"] as string;
            if (captchaStored == null || CaptchaInput == null ||
                !captchaStored.Equals(CaptchaInput, StringComparison.OrdinalIgnoreCase))
            {
                TempData["ErrorMessage"] = "Invalid CAPTCHA.";
                return View(model);
            }

            if (ModelState.IsValid)
            {
                // Check user credentials
                var user = _db.Logins.FirstOrDefault(u => u.UserID == model.UserID
                                                       && u.PasswordHash == model.Password);
                if (user != null)
                {
                    // Set session
                    Session["UserID"] = user.UserID;
                    Session["UserRole"] = user.Role;

                    // Role-based redirection
                    if (user.Role.Equals("Librarian", StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction("Dashboard", "Home");  // Librarian dashboard
                    }
                    else if (user.Role.Equals("Student", StringComparison.OrdinalIgnoreCase))
                    {
                        return RedirectToAction("Dashboard", "Students");  // Student dashboard
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home"); // Default page
                    }
                }

                TempData["ErrorMessage"] = "Invalid UserID or Password.";
            }

            return View(model);
        }


        // CAPTCHA Image
        public ActionResult CaptchaImage()
        {
            string captchaText = new Random().Next(1000, 9999).ToString();
            Session["Captcha"] = captchaText;

            using (Bitmap bmp = new Bitmap(120, 40))
            using (Graphics g = Graphics.FromImage(bmp))
            {
                g.Clear(Color.White);
                using (Font font = new Font("Arial", 20, FontStyle.Bold))
                using (SolidBrush brush = new SolidBrush(Color.Black))
                {
                    g.DrawString(captchaText, font, brush, 10, 5);
                }

                using (MemoryStream ms = new MemoryStream())
                {
                    bmp.Save(ms, ImageFormat.Png);
                    return File(ms.ToArray(), "image/png");
                }
            }
        }

        public ActionResult Logout()
        {
            Session.Clear();
            Session.Abandon();
            return RedirectToAction("Login", "Login");
        }
        // GET: Forget Password
        public ActionResult ForgetPassword()
        {
            return View();
        }
    }
}
