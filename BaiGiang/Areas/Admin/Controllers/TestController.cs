using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaiGiang.Repository;
namespace BaiGiang.Areas.Admin.Controllers
{
    public class TestController : Controller
    {
        //
        // GET: /Admin/Test/
        private BookRepository repository = new BookRepository();
        public ActionResult LoadAll(int? page)
        {
            var m = repository.GetBook(page);
            return View(m);
        }

    }
}
