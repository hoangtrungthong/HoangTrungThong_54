using HoangTrungThong_54.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace HoangTrungThong_54.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        Encryption encry = new Encryption();
        LTQLDb db = new LTQLDb();
        // GET: Account
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Register(Account acc)
        {
            if (ModelState.IsValid)
            {
                acc.Password = encry.PasswordEncryption(acc.Password);
                db.Accounts.Add(acc);
                db.SaveChanges();
                return RedirectToAction("Login", "Account");
            }
            return View(acc);
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Account acc)
        {
            if (ModelState.IsValid)
            {
                string encryptionpass = encry.PasswordEncryption(acc.Password);
                var model = db.Accounts.Where(m => m.Username == acc.Username && m.Password == encryptionpass).ToList().Count();
                if (model == 1)
                {
                    FormsAuthentication.SetAuthCookie(acc.Username, true);
                    return RedirectToAction("Index", "NhaCungCaps");
                    return RedirectToAction("Index", "SanPhams");
                   

                }
                else
                {
                    ModelState.AddModelError(" ", "Thông Tin Đăng Nhập Không Chính Xác");
                }
            }
            return View(acc);
        }
        public ActionResult SignOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }

    }
}