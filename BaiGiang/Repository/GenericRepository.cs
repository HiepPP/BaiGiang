using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaiGiang.Repository
{
    public interface GenericRepository<T> where T:class
    {
        IEnumerable<T> ChonTatCa();
        T ChonTheoId();
        void ThemMoi(T obj);
        void Sua(T obj);
        void Xoa(object Id);
        void Save();
    }
}