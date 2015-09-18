using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BaiGiang.Repository;
using System.Collections;
using PagedList;
using PagedList.Mvc;
using BaiGiang.Models;
using System.IO;
using BaiGiang.Models;
using System.Web.Security;
using System.Security;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Web.Helpers;
using Newtonsoft.Json;
using System.Net;
using Recaptcha;
namespace BaiGiang.Areas.Admin.Controllers
{

    public class AdminController : Controller
    {
        private BookRepository repository = new BookRepository();
        public AdminController()
        {
            this.repository = new BookRepository();
        }
        public AdminController(BookRepository repository)
        {
            this.repository = repository;
        }
        [Authorize]
        public ActionResult MainAdmin(int? page)
        {
            var m = repository.GetBook(page);
            return View(m);
        }
        [HttpGet]
        [Authorize]
        public ActionResult ThemSach()
        {
            ViewBag.HienThiLop = new SelectList(repository.ListLop(), "Id", "TenLop");
            ViewBag.HienThiMon = new SelectList(repository.ListMon(), "Id", "TenMon");
            ViewBag.HienThiLoai = new SelectList(repository.ListLoai(), "Id", "TenLoai");
            return View();
        }
        [Authorize]
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult ThemSach(Sach sach, HttpPostedFileBase imgUpload, HttpPostedFileBase fileUpload)
        {
            //var m = System.Web.HttpContext.Current.User.Identity;
            var m = User.Identity;
            BaiGiangContext db = new BaiGiangContext();
            var userlogin = db.Users.FirstOrDefault(x => x.UserName == System.Web.HttpContext.Current.User.Identity.Name);
            ViewBag.HienThiLop = new SelectList(repository.ListLop(), "Id", "TenLop", "Id");
            ViewBag.HienThiMon = new SelectList(repository.ListMon(), "Id", "TenMon", "Id");
            ViewBag.HienThiLoai = new SelectList(repository.ListLoai(), "Id", "TenLoai", "Id");
            if (imgUpload == null)
            {
                ViewBag.Mess = "Vui lòng chọn hình ảnh cho sách";
                return View();
            }
            if (fileUpload == null)
            {
                ViewBag.Mess = "Vui lòng chọn file cho sách";
                return View();
            }
            if (ModelState.IsValid)
            {
                //luu ten file
                var imgName = Path.GetFileName(imgUpload.FileName);
                var fileName = Path.GetFileName(fileUpload.FileName);
                //luu duong dan
                var pathImg = Path.Combine(Server.MapPath("/BookContent/BookImg"), imgName);
                var pathFile = Path.Combine(Server.MapPath("/BookContent/BookFile"), fileName);
                fileUpload.SaveAs(pathFile);
                //Kiem tra hinh anh da ton tai chua
                if (System.IO.File.Exists(pathImg))
                {
                    ViewBag.Mess = "Hình ảnh đã tồn tại";
                }
                else
                {
                    imgUpload.SaveAs(pathImg);
                }
            }
            //ViewBag.HienThiLop = sach.IdLop;
            sach.IdUser = userlogin.Id;
            sach.Img = imgUpload.FileName;
            sach.BookFile = fileUpload.FileName;
            sach.NgayDang = DateTime.Now;
            repository.Insert(sach);
            repository.Save();
            ViewBag.Mess = "Đã thêm sách thành công";
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult ThemMon()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemMon(Mon mon)
        {
            if (ModelState.IsValid)
            {
                repository.AddMon(mon);
                repository.Save();
                ViewBag.Mess = "Đã thêm thành công";
                return View();
            }
            ViewBag.Mess = "Dữ liệu nhập không chính xác, vui lòng nhập lại";
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult ThemLoaiSach()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemLoaiSach(LoaiSach loai)
        {
            if (ModelState.IsValid)
            {
                repository.AddLoai(loai);
                repository.Save();
                ViewBag.Mess = "Đã thêm thành công";
                return View();
            }
            ViewBag.Mess = "Dữ liệu nhập không chính xác, vui lòng nhập lại";
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult ThemLop()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ThemLop(Lop themlop)
        {
            if (ModelState.IsValid)
            {
                repository.AddLop(themlop);
                repository.Save();
                ViewBag.Mess = "Đã thêm thành công";
                return View();
            }
            ViewBag.Mess = "Dữ liệu nhập không chính xác, vui lòng nhập lại";
            return View();
        }
        [HttpGet]
        [Authorize]
        public ActionResult SuaMon()
        {
            var m = repository.GetMon();
            return View(m);
        }
        [HttpGet]
        [Authorize]
        public ActionResult ChiTietSuaMon(int id)
        {
            var m = repository.SelectMon(id);
            return View(m);
        }
        [HttpPost]
        [Authorize]
        public ActionResult ChiTietSuaMon(Mon mon)
        {
            repository.UpdateMon(mon);
            repository.Save();
            ViewBag.Mess = "Sửa thành công";
            return View();
        }
        [HttpPost]
        [Authorize]
        public ActionResult DeleteMon(Mon mon)
        {
            repository.DeleteMon(mon);
            repository.Save();
            return RedirectToAction("SuaMon", "Admin");
        }
        [HttpGet]
        [Authorize]
        public ActionResult SuaSach(int id)
        {
            ViewBag.HienThiMon = new SelectList(repository.ListMon(), "Id", "TenMon", "Id");
            ViewBag.HienThiLop = new SelectList(repository.ListLop(), "Id", "TenLop", "Id");
            ViewBag.HienThiLoai = new SelectList(repository.ListLoai(), "Id", "TenLoai", "Id");
            var m = repository.SelectById(id);
            return View(m);
        }
        [HttpPost]
        [ValidateInput(false)]
        [Authorize]
        public ActionResult SuaSach(Sach sach, HttpPostedFileBase imgUpload, HttpPostedFileBase fileUpload)
        {
            ViewBag.HienThiMon = new SelectList(repository.ListMon(), "Id", "TenMon", "Id");
            ViewBag.HienThiLop = new SelectList(repository.ListLop(), "Id", "TenLop", "Id");
            ViewBag.HienThiLoai = new SelectList(repository.ListLoai(), "Id", "TenLoai", "Id");
            if (imgUpload == null)
            {
                ViewBag.Mess = "Vui lòng chọn hình ảnh cho sách";
                return SuaSach(sach.Id);
            }
            if (fileUpload == null)
            {
                ViewBag.Mess = "Vui lòng chọn file cho sách";
                return SuaSach(sach.Id);
            }
            if (ModelState.IsValid)
            {
                //luu ten file
                var imgName = Path.GetFileName(imgUpload.FileName);
                var fileName = Path.GetFileName(fileUpload.FileName);
                //luu duong dan
                var pathImg = Path.Combine(Server.MapPath("/BookContent/BookImg"), imgName);
                var pathFile = Path.Combine(Server.MapPath("/BookContent/BookFile"), fileName);
                fileUpload.SaveAs(pathFile);
                //Kiem tra hinh anh da ton tai chua
                if (System.IO.File.Exists(pathImg))
                {
                    ViewBag.Mess = "Hình ảnh đã tồn tại";
                }
                else
                {
                    imgUpload.SaveAs(pathImg);
                }
            }
            //ViewBag.HienThiLop = sach.IdLop;
            sach.Img = imgUpload.FileName;
            sach.BookFile = fileUpload.FileName;
            repository.Update(sach);
            repository.Save();
            return RedirectToAction("MainAdmin", "Admin");
        }
        public ActionResult XoaSach(Sach sach)
        {
            repository.DeleteSach(sach);
            repository.Save();
            return RedirectToAction("MainAdmin", "Admin");
        }
        [HttpGet]
        [Authorize]
        public ActionResult SuaLoaiSach(int? page)
        {
            var m = repository.GetLoaiSach(page);
            return View(m);
        }
        [HttpGet]
        [Authorize]
        public ActionResult ChiTietSuaLoai(int id)
        {
            var m = repository.SelectLoai(id);
            return View(m);
        }
        [HttpPost]
        public ActionResult ChiTietSuaLoai(LoaiSach loai)
        {
            repository.UpdateLoai(loai);
            repository.Save();
            return RedirectToAction("SuaLoaiSach", "Admin");
        }
        [HttpPost]
        public ActionResult XoaLoai(LoaiSach loai)
        {
            repository.DeleteLoai(loai);
            repository.Save();
            return RedirectToAction("SuaLoaiSach", "Admin");
        }
        [HttpGet]
        public ActionResult DangNhap()
        {
            return View();
        }
        [HttpPost, RecaptchaControlMvc.CaptchaValidator]
        public ActionResult DangNhap(User user, bool captchaValid)
        {
            if (!captchaValid)
            {
                return View();
            }
            var check = repository.CheckRole(user);
            if (check ==1 )
            {
                return RedirectToAction("MainAdmin", "Admin");
            }
            else if(check == 2 )
            {
                ViewBag.ThongBao = "Tài khoản của bạn đã bị khóa";
            }
            else if (check == 3)
            {
                ViewBag.ThongBao = "Tài khoản của bạn đã bị khóa";
            }
            else if(check == 4)
            {
                ViewBag.Mess = "Thông tin đăng nhập không chính xác, vui lòng đăng nhập lại";
            }
            return View();
            ////BaiGiangContext db = new BaiGiangContext();
            //if (ModelState.IsValid)
            //{
            //    var m = repository.CheckRole(user);
            //    if (m != null)
            //    {
            //        FormsAuthentication.SetAuthCookie(m.UserName, false);
            //        return RedirectToAction("MainAdmin", "Admin");
            //    }
            //}
            //ViewBag.Mess = "Thông tin đăng nhập không chính xác, vui lòng đăng nhập lại"; 
            //return View();
        }
        public class CaptchaResponse
        {
            [JsonProperty("success")]
            public bool Success { get; set; }

            [JsonProperty("error-codes")]
            public List<string> ErrorCodes { get; set; }
        }
        public ActionResult Thoat()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home", new { area = "" });
        }
        #region encrypt
        const string passphrase = "Password@123";
        public string encrypt(string message)
        {
            byte[] results;
            UTF8Encoding utf8 = new UTF8Encoding();
            //to create the object for UTF8Encoding  class
            //TO create the object for MD5CryptoServiceProvider 
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] deskey = md5.ComputeHash(utf8.GetBytes(passphrase));
            //to convert to binary passkey
            //TO create the object for  TripleDESCryptoServiceProvider 
            TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider();
            desalg.Key = deskey;//to  pass encode key
            desalg.Mode = CipherMode.ECB;
            desalg.Padding = PaddingMode.PKCS7;
            byte[] encrypt_data = utf8.GetBytes(message);
            //to convert the string to utf encoding binary 
            try
            {
                //To transform the utf binary code to md5 encrypt    
                ICryptoTransform encryptor = desalg.CreateEncryptor();
                results = encryptor.TransformFinalBlock(encrypt_data, 0, encrypt_data.Length);
            }
            finally
            {
                //to clear the allocated memory
                desalg.Clear();
                md5.Clear();
            }
            //to convert to 64 bit string from converted md5 algorithm binary code
            return Convert.ToBase64String(results);
        }
        #endregion
        #region decrypt
        public string decrypt(string message)
        {
            byte[] results;
            UTF8Encoding utf8 = new UTF8Encoding();
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            byte[] deskey = md5.ComputeHash(utf8.GetBytes(passphrase));
            TripleDESCryptoServiceProvider desalg = new TripleDESCryptoServiceProvider();
            desalg.Key = deskey;
            desalg.Mode = CipherMode.ECB;
            desalg.Padding = PaddingMode.PKCS7;
            byte[] decrypt_data = Convert.FromBase64String(message);
            try
            {
                //To transform the utf binary code to md5 decrypt
                ICryptoTransform decryptor = desalg.CreateDecryptor();
                results = decryptor.TransformFinalBlock(decrypt_data, 0, decrypt_data.Length);
            }
            finally
            {
                desalg.Clear();
                md5.Clear();
            }
            //TO convert decrypted binery code to string
            return utf8.GetString(results);
        }
        #endregion
    }
}
