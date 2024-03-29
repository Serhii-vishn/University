﻿namespace University.Models.ModelsConfiguration
{
    public class UserTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

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
                .HasMaxLength(50);

            builder.Property(u => u.Role)
                    .IsRequired()
                    .HasMaxLength(7);

            builder.HasOne(u => u.Human)
                   .WithOne(h => h.User)
                   .HasForeignKey<User>(u => u.HumanId);
        }
    }
}
