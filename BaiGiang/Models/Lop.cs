using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BaiGiang.Models
{
    public partial class Lop
    {
        public Lop()
        {
            this.Saches = new List<Sach>();
        }

        public int Id { get; set; }
        [Required]
        public string TenLop { get; set; }
        public virtual ICollection<Sach> Saches { get; set; }
    }
}
