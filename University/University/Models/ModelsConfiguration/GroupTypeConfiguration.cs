using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University.Models.ModelsConfiguration
{
    public class GroupTypeConfiguration : IEntityTypeConfiguration<Group>
    {
        public void Configure(EntityTypeBuilder<Group> builder)
        {
            builder.ToTable("Group");

            builder.HasKey(g => g.Id);

            builder.Property(g => g.Id)
                    .IsRequired()
                    .UseIdentityColumn();

            builder.Property(g => g.GroupName)
                    .IsRequired()
                    .HasMaxLength(10);

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
