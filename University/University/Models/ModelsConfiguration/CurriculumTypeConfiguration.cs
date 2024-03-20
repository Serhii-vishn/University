namespace University.Models.ModelsConfiguration
{
    public class CurriculumTypeConfiguration : IEntityTypeConfiguration<Curriculum>
    {
        public void Configure(EntityTypeBuilder<Curriculum> builder)
        {
            builder.ToTable("Curriculum");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                    .IsRequired()
                    .UseIdentityColumn();

            builder.Property(d => d.Name)
                    .IsRequired()
                    .HasMaxLength(50);

            builder.HasIndex(d => d.Name)
                    .IsUnique();

            builder.Property(d => d.Description)
                    .IsRequired(false)
                    .HasMaxLength(100);

            builder.HasOne(d => d.Faculty)
                    .WithMany(f => f.Curriculums)
                    .HasForeignKey(f => f.FacultyId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Groups)
                    .WithOne(g => g.Curriculum)
                    .HasForeignKey(g => g.CurriculumId)
                    .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(d => d.Teachers)
                    .WithMany(t => t.Curriculums)
                    .UsingEntity(j => j.ToTable("TeacherCurriculum"));
        }
    }
}
