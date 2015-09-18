using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using BaiGiang.Models.Mapping;

namespace BaiGiang.Models
{
    public partial class BaiGiangContext : DbContext
    {
        static BaiGiangContext()
        {
            Database.SetInitializer<BaiGiangContext>(null);
        }

        public BaiGiangContext()
            : base("Name=BaiGiangContext")
        {
        }

        public DbSet<LoaiSach> LoaiSaches { get; set; }
        public DbSet<Lop> Lops { get; set; }
        public DbSet<Mon> Mons { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Sach> Saches { get; set; }
        public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new LoaiSachMap());
            modelBuilder.Configurations.Add(new LopMap());
            modelBuilder.Configurations.Add(new MonMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new SachMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new UserMap());
        }
    }
}
