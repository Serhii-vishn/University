using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace University.Models.ModelsConfiguration
{
    public class BuildingTypeConfiguration : IEntityTypeConfiguration<Building>
    {
        public void Configure(EntityTypeBuilder<Building> builder)
        {
            builder.ToTable("Builder");

            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                    .IsRequired()
                    .UseIdentityColumn();

            builder.Property(b => b.BuildingNumber)
                    .IsRequired();

            builder.Property(b => b.CapacityRooms)
                    .IsRequired();

            builder.Property(b => b.Address)
                    .IsRequired()
                    .HasMaxLength(40);
        }
    }
}
