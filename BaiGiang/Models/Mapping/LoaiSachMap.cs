using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BaiGiang.Models.Mapping
{
    public class LoaiSachMap : EntityTypeConfiguration<LoaiSach>
    {
        public LoaiSachMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TenLoai)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("LoaiSach");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TenLoai).HasColumnName("TenLoai");
            this.Property(t => t.ChiTiet).HasColumnName("ChiTiet");
        }
    }
}
