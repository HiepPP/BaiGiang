using System;
using System.Collections.Generic;

namespace BaiGiang.Models
{
    public partial class Role
    {
        public Role()
        {
            this.Users = new List<User>();
        }

        public int Id { get; set; }
        public string RoleName { get; set; }
        public string ChiTiet { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
