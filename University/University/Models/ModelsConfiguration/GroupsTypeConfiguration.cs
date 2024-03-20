namespace University.Models.ModelsConfiguration
{
    public class GroupsTypeConfiguration : IEntityTypeConfiguration<Groups>
    {
        public void Configure(EntityTypeBuilder<Groups> builder)
        {
            builder.ToTable("Groups");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                    .IsRequired()
                    .UseIdentityColumn();

            builder.Property(g => g.GroupName)
                    .IsRequired()
                    .HasMaxLength(10);
            
            builder.HasIndex(u => u.GroupName)
                    .IsUnique();

            builder.HasOne(g => g.Teacher)
                    .WithMany(t => t.Groups)
                    .HasForeignKey(g => g.CuratorId)
                    .IsRequired(false);

            builder.HasOne(d => d.Curriculum)
                    .WithMany(g => g.Groups)
                    .HasForeignKey(d => d.CurriculumId);

            builder.HasMany(s => s.Students)
                    .WithOne(g => g.Group)
                    .HasForeignKey(g => g.GroupId)
                    .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
