using System;
using System.Collections.Generic;

namespace BaiGiang.Models
{
    public partial class Mon
    {
        public Mon()
        {
            this.Saches = new List<Sach>();
        }

        public int Id { get; set; }
        public string TenMon { get; set; }
        public string ChiTiet { get; set; }
        public virtual ICollection<Sach> Saches { get; set; }
    }
}
