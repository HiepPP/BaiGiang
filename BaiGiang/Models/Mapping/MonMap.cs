using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BaiGiang.Models.Mapping
{
    public class MonMap : EntityTypeConfiguration<Mon>
    {
        public MonMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.TenMon)
                .HasMaxLength(150);

            // Table & Column Mappings
            this.ToTable("Mon");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.TenMon).HasColumnName("TenMon");
            this.Property(t => t.ChiTiet).HasColumnName("ChiTiet");
        }
    }
}
