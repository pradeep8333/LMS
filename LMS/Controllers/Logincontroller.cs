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
                // Check user credentials (any role)
                var user = _db.Logins.FirstOrDefault(u => u.UserID == model.UserID
                                                       && u.PasswordHash == model.Password);
                if (user != null)
                {
                    // Set session
                    Session["UserID"] = user.UserID;
                    Session["UserRole"] = user.Role;

                    // Redirect to LMS home/dashboard
                    return RedirectToAction("HomePage", "Home");
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

        // GET: Forget Password
        public ActionResult ForgetPassword()
        {
            return View();
        }
    }
}
