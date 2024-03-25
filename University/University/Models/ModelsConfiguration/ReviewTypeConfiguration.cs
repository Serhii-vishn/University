
namespace University.Models.ModelsConfiguration
{
    public class ReviewTypeConfiguration : IEntityTypeConfiguration<Review>
    {
        public void Configure(EntityTypeBuilder<Review> builder)
        {
            builder.ToTable("Review");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                    .IsRequired()
                    .UseIdentityColumn();

            builder.Property(r => r.FeedBack)
                    .IsRequired()
                    .HasMaxLength(100);

            builder.Property(r => r.PostDate)
                    .IsRequired()
                    .HasColumnType("smalldatetime");

            builder.HasOne(r => r.Teacher)
                    .WithMany(t => t.Reviews)
                    .HasForeignKey(r => r.TeacherId)
                    .IsRequired(false);

            builder.HasOne(r => r.Student)
                    .WithMany(t => t.Reviews)
                    .HasForeignKey(r => r.StudentId)
                    .IsRequired(false);
        }
    }
}
