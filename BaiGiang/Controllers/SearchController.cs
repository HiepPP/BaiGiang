using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaiGiang.Models;
using BaiGiang.Models.ViewModel;
using PagedList.Mvc;
using PagedList;
using BaiGiang.Repository;
namespace BaiGiang.Controllers
{
    public class SearchController : Controller
    {
        //
        // GET: /Search/
         private BookRepository repository = new BookRepository();
        public SearchController()
        {
            this.repository = new BookRepository();
        }
        public SearchController(BookRepository repository)
        {
            this.repository = repository;
        }
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult TimKiemPar()
        {
            return PartialView();
        }
        [ValidateInput(false)]
        public ActionResult TimKiem(string tukhoa, int? page)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.tuukhoa = tukhoa;
            var m = repository.Search(tukhoa, page);
            return View(m);
        }
    }
}
