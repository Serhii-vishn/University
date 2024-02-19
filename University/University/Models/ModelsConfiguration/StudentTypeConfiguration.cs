﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University.Models.ModelsConfiguration
{
    public class StudentTypeConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Student");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                    .IsRequired()
                    .UseIdentityColumn();

            builder.Property(s => s.Course)
                    .IsRequired();

            builder.Property(s => s.Speciality)
                    .IsRequired()
                    .HasMaxLength(30);

            builder.HasOne(s => s.Group)
                       .WithMany(g => g.Students)
                       .HasForeignKey(s => s.GroupId);
        }
    }
}
