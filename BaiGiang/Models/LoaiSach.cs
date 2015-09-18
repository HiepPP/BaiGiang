using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaiGiang.Models
{
    public partial class LoaiSach
    {
        public LoaiSach()
        {
            this.Saches = new List<Sach>();
        }

        public int Id { get; set; }
        [Required]
        public string TenLoai { get; set; }
        public string ChiTiet { get; set; }
        public virtual ICollection<Sach> Saches { get; set; }
    }
}
