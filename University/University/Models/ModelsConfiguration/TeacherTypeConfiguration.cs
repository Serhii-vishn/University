using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University.Models.ModelsConfiguration
{
    public class TeacherTypeConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("Teacher");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Position)
                    .IsRequired()
                    .HasMaxLength(40);

            builder.Property(t => t.AcademicDegreee)
                    .IsRequired()
                    .HasMaxLength(40);
        }
    }
}
