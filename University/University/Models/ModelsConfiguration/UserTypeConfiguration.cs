using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University.Models.ModelsConfiguration
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .IsRequired()
                .UseIdentityColumn();

            builder.Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(50);

            builder.HasIndex(u => u.Login)
                .IsUnique();
               
            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(50)
                .IsFixedLength();

            builder.HasOne(u => u.Human)
                   .WithOne(h => h.User)
                   .HasForeignKey<Human>(h => h.UserId);
        }
    }
}
