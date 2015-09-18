using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BaiGiang.Models.ViewModel
{
    public class SearchViewModel
    {
        public string tukhoa { get; set; }

        public int Id { get; set; }
        public string TenSach { get; set; }
        public Nullable<int> Rating { get; set; }
        public Nullable<int> LuotXem { get; set; }
        public string GioiThieu { get; set; }
        public string BookFile { get; set; }
        public string Img { get; set; }
        public Nullable<System.DateTime> NgayDang { get; set; }
        public Nullable<int> IdLop { get; set; }
        public Nullable<int> IdMon { get; set; }
        public Nullable<int> IdUser { get; set; }
        public virtual Lop Lop { get; set; }
        public virtual Mon Mon { get; set; }
        public virtual User User { get; set; }
        
    }
}