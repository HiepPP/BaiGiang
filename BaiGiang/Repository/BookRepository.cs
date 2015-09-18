using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaiGiang.Models;
using System.Collections;
using PagedList;
using PagedList.Mvc;
using System.Web.Mvc;
using System.Web.Security;
using System.Security;
using System.Text;
using System.Security.Cryptography;

namespace BaiGiang.Repository
{
    public class BookRepository : IBookRepository
    {
        private BaiGiangContext db { get; set; }
        public BookRepository()
        {
            this.db = new BaiGiangContext();
        }
        public BookRepository(BaiGiangContext db)
        {
            this.db = db;
        }
        public IEnumerable GetBook(int? page)
        {
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return db.Saches.OrderByDescending(x=>x.NgayDang).ToPagedList(pageNumber, pageSize);
        }
        public IEnumerable GetMon()
        {
            return db.Mons.ToList();
        }
        public Sach SelectById(int id)
        {
            var m = db.Saches.FirstOrDefault(x => x.Id == id);
            return m;
        }
        public Mon SelectMon(int id)
        {
            var m = db.Mons.FirstOrDefault(x => x.Id == id);
            return m;
        }
        public IEnumerable GetLoaiSach(int? page)
        {
            int pageSize = 12;
            int pageNumber = (page ?? 1);
            return db.LoaiSaches.ToList().ToPagedList(pageNumber, pageSize);
        }
        public LoaiSach SelectLoai(int id)
        {
            var m = db.LoaiSaches.SingleOrDefault(x => x.Id == id);
            return m;
        }
        public void Insert(Sach obj)
        {
            db.Saches.Add(obj);
        }
        public void AddMon(Mon mon)
        {
            db.Mons.Add(mon);
        }
        public void AddLoai(LoaiSach loaisach)
        {
            db.LoaiSaches.Add(loaisach);
        }
        public void AddLop(Lop lop)
        {
            db.Lops.Add(lop);
        }
        public void Update(Sach obj)
        {
            db.Entry(obj).State = System.Data.EntityState.Modified;
        }
        public void UpdateMon(Mon mon)
        {
            db.Entry(mon).State = System.Data.EntityState.Modified;
        }
        public void UpdateLoai(LoaiSach loai)
        {
            db.Entry(loai).State = System.Data.EntityState.Modified;
        }
        public void Delete(int id)
        {
            Sach existid = db.Saches.Find(id);
            db.Saches.Remove(existid);
        }
        public void DeleteMon(Mon mon)
        {
            var m = db.Mons.Find(mon.Id);
            db.Mons.Remove(m);
        }
        public void DeleteSach(Sach sach)
        {
            var m = db.Saches.Find(sach.Id);
            db.Saches.Remove(m);
        }
        public void DeleteLoai(LoaiSach loai)
        {
            var m = db.LoaiSaches.Find(loai.Id);
            db.LoaiSaches.Remove(m);
        }
        public void Save()
        {
            db.SaveChanges();
        }
        public IEnumerable GetNewBook()
        {
            var m = db.Saches.Take(3).ToList().OrderByDescending(x => x.NgayDang);
            return m;
        }
        public IEnumerable SachLienQuan(int idmon)
        {
            var m = db.Saches.Where(x => x.IdMon == idmon).ToList().Take(12);
            return m;
        }
        public IEnumerable SachTheoLop(int idlop)
        {
            var m = db.Saches.Where(x => x.IdLop == idlop).ToList();
            return m;
        }
        public IEnumerable Sort(string loai, int? page)
        {
            int pageNumer = (page ?? 1);
            int pageSize = 12;
            List<Sach> sort = new List<Sach>();
            if (loai == "newest")
            {
                sort = db.Saches.OrderByDescending(x => x.NgayDang).ToList();
            }
            if (loai == "like")
            {
                sort = db.Saches.OrderByDescending(x => x.Rating).ToList();
            }
            if (loai == "view")
            {
                sort = db.Saches.OrderByDescending(x => x.LuotXem).ToList();
            }
            IEnumerable<Sach> sach = sort;
            return sach.ToPagedList(pageNumer, pageSize);
        }
        public IEnumerable Search(string tukhoa, int? page)
        {
            int pageNumber = (page ?? 1);
            int pageSize = 12;
            var m = db.Saches.Where(x => x.GioiThieu.Contains(tukhoa)).ToList();
            return m.ToPagedList(pageNumber, pageSize);
        }
        public IEnumerable ListMon()
        {
            var m = db.Mons.ToList();
            return m;
        }
        public IEnumerable ListLop()
        {
            var m = db.Lops.ToList();
            return m;
        }
        public IEnumerable ListLoai()
        {
            var m = db.LoaiSaches.ToList();
            return m;
        }
        //admin repo
        #region MD5
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
        #endregion
        public int CheckRole(User user)
        {
                var pass = encrypt(user.Password);
                var m = db.Users.Where(x => x.UserName == user.UserName && x.Password == pass && x.IdRole == 1).SingleOrDefault();
                var n = db.Users.Where(x => x.UserName.Equals(user.UserName) && x.Password != user.Password && x.IdRole == 1).SingleOrDefault();
                if (m!=null && m.LoginFail <= 3)
                {
                    var p = decrypt(m.Password);
                    FormsAuthentication.SetAuthCookie(m.UserName.ToString(), false);
                    return 1;
                }
                else if (n != null && n.LoginFail < 3)
                {
                    n.LoginFail++;
                    db.SaveChanges();
                    return 0;
                }
                else if (m != null && m.LoginFail >= 3)
                {
                    return 2;
                }
                else if (n != null && n.LoginFail >= 3)
                {
                    return 3;
                }
                return 0;
        }
    }
}