using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BaiGiang.Models.Mapping
{
    public class SachMap : EntityTypeConfiguration<Sach>
    {
        public SachMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TenSach)
                .HasMaxLength(200);

            this.Property(t => t.Img)
                .IsFixedLength()
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("Sach");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TenSach).HasColumnName("TenSach");
            this.Property(t => t.Rating).HasColumnName("Rating");
            this.Property(t => t.LuotXem).HasColumnName("LuotXem");
            this.Property(t => t.GioiThieu).HasColumnName("GioiThieu");
            this.Property(t => t.BookFile).HasColumnName("BookFile");
            this.Property(t => t.Img).HasColumnName("Img");
            this.Property(t => t.NgayDang).HasColumnName("NgayDang");
            this.Property(t => t.IdLop).HasColumnName("IdLop");
            this.Property(t => t.IdMon).HasColumnName("IdMon");
            this.Property(t => t.IdUser).HasColumnName("IdUser");
            this.Property(t => t.IdLoaiSach).HasColumnName("IdLoaiSach");

            // Relationships
            this.HasOptional(t => t.LoaiSach)
                .WithMany(t => t.Saches)
                .HasForeignKey(d => d.IdLoaiSach);
            this.HasOptional(t => t.Lop)
                .WithMany(t => t.Saches)
                .HasForeignKey(d => d.IdLop);
            this.HasOptional(t => t.Mon)
                .WithMany(t => t.Saches)
                .HasForeignKey(d => d.IdMon);
            this.HasOptional(t => t.User)
                .WithMany(t => t.Saches)
                .HasForeignKey(d => d.IdUser);

        }
    }
}
