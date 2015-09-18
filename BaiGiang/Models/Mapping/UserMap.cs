using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace BaiGiang.Models.Mapping
{
    public class UserMap : EntityTypeConfiguration<User>
    {
        public UserMap()
        {
            // Primary Key
            this.HasKey(t => t.Id);

            // Properties
            this.Property(t => t.UserName)
                .IsFixedLength()
                .HasMaxLength(20);

            this.Property(t => t.Password)
                .IsFixedLength()
                .HasMaxLength(250);

            this.Property(t => t.Truong)
                .HasMaxLength(150);

            this.Property(t => t.HoTen)
                .HasMaxLength(100);

            // Table & Column Mappings
            this.ToTable("User");
            this.Property(t => t.Id).HasColumnName("Id");
            this.Property(t => t.IdRole).HasColumnName("IdRole");
            this.Property(t => t.UserName).HasColumnName("UserName");
            this.Property(t => t.Password).HasColumnName("Password");
            this.Property(t => t.Truong).HasColumnName("Truong");
            this.Property(t => t.HoTen).HasColumnName("HoTen");
            this.Property(t => t.LoginFail).HasColumnName("LoginFail");

            // Relationships
            this.HasOptional(t => t.Role)
                .WithMany(t => t.Users)
                .HasForeignKey(d => d.IdRole);

        }
    }
}
