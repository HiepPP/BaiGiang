using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaiGiang.Repository;
using BaiGiang.Models;
using PagedList;
using PagedList.Mvc;
using BaiGiang.Models.ViewModel;
namespace BaiGiang.Controllers
{
    public class HomeController : Controller
    {
        //private IBookRepository repository = null;
        private BookRepository repository = new BookRepository();
        public HomeController()
        {
            this.repository = new BookRepository();
        }
        public HomeController(BookRepository repository)
        {
            this.repository = repository;
        }
        //
        // GET: /Home/

        public ActionResult Index(int? page)
        {
            var model = repository.GetBook(page);
            return View(model);
        }
        public ActionResult Detail(int id)
        {
            Sach model = repository.SelectById(id);
            return View(model);
        }
        public ActionResult SachMoi()
        {
            var m = repository.GetNewBook();
            return PartialView(m);
        }
        public ActionResult LienQuan(int idmon)
        {
            var m = repository.SachLienQuan(idmon);
            return PartialView(m);
        }
        public ActionResult TheoLop(int idlop)
        {
            var theolop = repository.SachTheoLop(idlop);
            if (idlop >= 13)
            {
                return RedirectToAction("NotFound", "Home");
            }

            return View(theolop);
        }
        public ActionResult NotFound()
        {
            return View();
        }
        public ActionResult Sort(string loai, int? page)
        {
            ViewBag.type = loai;
            var m = repository.Sort(loai, page);
            return View(m);
        }
    }
}
