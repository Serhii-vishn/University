using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University.Models.ModelsConfiguration
{
    public class DepartmentTypeConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.ToTable("Department");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                    .IsRequired()
                    .UseIdentityColumn();

            builder.Property(d => d.Name)
                    .IsRequired()
                    .HasMaxLength(25);

            builder.HasIndex(d => d.Name)
                    .IsUnique();

            builder.Property(d => d.Description)
                    .IsRequired(false)
                    .HasMaxLength(100);

            builder.HasOne(d => d.Faculty)
                    .WithMany(f => f.Departments)
                    .HasForeignKey(f => f.FacultyId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Groups)
                    .WithOne(g => g.Department)
                    .HasForeignKey(g => g.DepartmentId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Teachers)
                    .WithMany(t => t.Departments)
                    .UsingEntity(j => j.ToTable("TeacherDepartment"));
        }
    }
}
