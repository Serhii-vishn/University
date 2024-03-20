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
                    .HasMaxLength(50);

            builder.HasIndex(f => f.Name)
                    .IsUnique();

            builder.Property(f => f.Description)
                    .IsRequired(false)
                    .HasMaxLength(100);

            builder.HasMany(f => f.Curriculums)
                    .WithOne(d => d.Faculty)
                    .HasForeignKey(d => d.FacultyId)
                    .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
