using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BaiGiang.Models.Mapping
{
    public class LopMap : EntityTypeConfiguration<Lop>
    {
        public LopMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TenLop)
                .HasMaxLength(200);

            // Table & Column Mappings
            this.ToTable("Lop");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TenLop).HasColumnName("TenLop");
        }
    }
}
