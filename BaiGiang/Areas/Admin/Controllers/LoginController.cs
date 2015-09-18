using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaiGiang.Models;
namespace BaiGiang.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        //
        // GET: /Admin/Login/
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        
        //public Action Login(User user)
        //{
        //    BaiGiangContext db = new BaiGiangContext();
        //    var m = db.Users.SingleOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
        //    if (m != null)
        //    {
        //        return RedirectToAction("MainAdmin", "Admin");
        //    }
        //    return View();
        //}

    }
}
