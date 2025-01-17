using EncantoApadrinhamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EncantoApadrinhamento.Infra.Mappings
{
    public class AddressEntityConfiguration : IEntityTypeConfiguration<AddressEntity>
    {
        public void Configure(EntityTypeBuilder<AddressEntity> builder)
        {
            builder.Property(x => x.Street)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Number)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(x => x.Complement)
                .HasMaxLength(100);

            builder.Property(x => x.Neighborhood)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.City)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.State)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.Country)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.ZipCode)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(x => x.User)
                .WithOne(x => x.Address)
                .HasForeignKey<AddressEntity>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired(false);
        }
    }
}
