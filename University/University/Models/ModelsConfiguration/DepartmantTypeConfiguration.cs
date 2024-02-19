using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University.Models.ModelsConfiguration
{
    public class DepartmantTypeConfiguration : IEntityTypeConfiguration<Departmant>
    {
        public void Configure(EntityTypeBuilder<Departmant> builder)
        {
            builder.ToTable("Departmant");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                    .IsRequired()
                    .UseIdentityColumn();

            builder.Property(d => d.Name)
                    .IsRequired()
                    .HasMaxLength(25);

            builder.Property(d => d.Description)
                    .IsRequired(false)
                    .HasMaxLength(100);

            builder.HasOne(d => d.Faculty)
                .WithMany(f => f.Departmants)
                .HasForeignKey(f => f.FacultyId);

            builder.HasMany(d => d.Groups)
                   .WithOne(g => g.Departmant)
                   .HasForeignKey(g => g.DepartmantId);
        }
    }
}
