using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University.Models.ModelsConfiguration
{
    public class HumanTypeConfiguration : IEntityTypeConfiguration<Human>
    {
        public void Configure(EntityTypeBuilder<Human> builder)
        {
            builder.ToTable("Human");

            builder.HasKey(h => h.Id);

            builder.Property(h => h.Id)
                    .IsRequired()
                    .UseIdentityColumn();

            builder.Property(h => h.LastName)
                    .IsRequired()
                    .HasMaxLength(30);

            builder.Property(h => h.FirstName)
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

            builder.Property(h => h.Address)
                    .IsRequired(false)
                    .HasMaxLength(50);

            builder.Property(h => h.Email)
                    .IsRequired(false)
                    .HasMaxLength(35);

            builder.Property(h => h.Phone)
                    .IsRequired(false)
                    .HasMaxLength(13)
                    .IsFixedLength();

            builder.HasOne(t => t.Teacher)
                    .WithOne(h => h.Human)
                    .HasForeignKey<Teacher>(h => h.HumanId);

            builder.HasOne(s => s.Student)
                    .WithOne(h => h.Human)
                    .HasForeignKey<Student>(h => h.HumanId);
        }
    }
}
