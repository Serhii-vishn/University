using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University.Models.ModelsConfiguration
{
    public class HumanTypeConfiguration : IEntityTypeConfiguration<Human>
    {
        public void Configure(EntityTypeBuilder<Human> builder)
        {
            builder.ToTable("Human");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                    .IsRequired()
                    .UseIdentityColumn();

            builder.Property(u => u.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

            builder.Property(u => u.FirstName)
                    .IsRequired()
                    .HasMaxLength(30);

            builder.Property(h => h.DateOfBirth)
                    .IsRequired()
                    .HasColumnType("date");

            builder.HasIndex(h => new { h.LastName, h.FirstName, h.DateOfBirth })
                    .IsUnique();

            builder.Property(h => h.Gender)
                    .IsRequired(false)
                    .HasMaxLength(10);

            builder.Property(u => u.Address)
                    .IsRequired(false)
                    .HasMaxLength(50);

            builder.Property(u => u.Email)
                    .IsRequired(false)
                    .HasMaxLength(35);

            builder.Property(u => u.Phone)
                    .IsRequired(false)
                    .HasMaxLength(13)
                    .IsFixedLength();

            builder.HasOne(h => h.User)
                    .WithOne(u => u.Human)
                    .HasForeignKey<User>(u => u.HumanId);
        }
    }
}
