using System;
using System.Collections.Generic;

namespace BaiGiang.Models
{
    public partial class Sach
    {
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
        public Nullable<int> IdLoaiSach { get; set; }
        public virtual LoaiSach LoaiSach { get; set; }
        public virtual Lop Lop { get; set; }
        public virtual Mon Mon { get; set; }
        public virtual User User { get; set; }
    }
}
