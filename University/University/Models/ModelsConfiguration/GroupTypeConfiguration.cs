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
                    .HasMaxLength(30);

            builder.HasOne(d => d.Departmant)
                    .WithMany(g => g.Groups)
                    .HasForeignKey(d => d.DepartmantId);

            builder.HasMany(s => s.Students)
                    .WithOne(g => g.Group)
                    .HasForeignKey(g => g.GroupId);
        }
    }
}
