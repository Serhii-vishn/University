using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University.Models.ModelsConfiguration
{
    public class FacultyTypeConfiguration : IEntityTypeConfiguration<Faculty>
    {
        public void Configure(EntityTypeBuilder<Faculty> builder)
        {
            builder.ToTable("Faculty");

            builder.HasKey(f => f.Id);

            builder.Property(f => f.Id)
                    .IsRequired()
                    .UseIdentityColumn();

            builder.Property(f => f.Name)
                    .IsRequired()
                    .HasMaxLength(25);

            builder.Property(f => f.Description)
                    .IsRequired(false)
                    .HasMaxLength(100);

            builder.HasMany(f => f.Departmants)
                       .WithOne(d => d.Faculty)
                       .HasForeignKey(d => d.FacultyId)
                       .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
