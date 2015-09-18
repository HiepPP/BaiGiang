 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BaiGiang.Models;
using System.Collections;

namespace BaiGiang.Repository
{
    public interface IBookRepository
    {
        IEnumerable GetBook(int? page);
        Sach SelectById(int id);
        IEnumerable GetNewBook();
        IEnumerable SachLienQuan(int idmon);
        IEnumerable SachTheoLop(int idlop);
        IEnumerable Sort(string loai, int? page);
        void Insert(Sach obj);
        void Update(Sach obj);
        void Delete(int id);
        void Save();

    }
}