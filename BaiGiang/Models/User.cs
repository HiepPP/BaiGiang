using System;
using System.Collections.Generic;

namespace BaiGiang.Models
{
    public partial class User
    {
        public User()
        {
            this.Saches = new List<Sach>();
        }

        public int Id { get; set; }
        public Nullable<int> IdRole { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Truong { get; set; }
        public string HoTen { get; set; }
        public Nullable<int> LoginFail { get; set; }
        public virtual Role Role { get; set; }
        public virtual ICollection<Sach> Saches { get; set; }
    }
}
