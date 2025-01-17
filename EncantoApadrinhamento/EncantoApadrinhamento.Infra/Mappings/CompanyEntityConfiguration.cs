using EncantoApadrinhamento.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EncantoApadrinhamento.Infra.Mappings
{
    public class CompanyEntityConfiguration : IEntityTypeConfiguration<CompanyEntity>
    {
        public void Configure(EntityTypeBuilder<CompanyEntity> builder)
        {
            builder.ToTable("Companies");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.SocialName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.FantasyName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.CompanyType)
                .IsRequired();

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(x => x.Cnpj)
                .IsRequired()
                .HasMaxLength(18);

            builder.Property(x => x.Phone)
                .IsRequired()
                .HasMaxLength(15);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(x => x.RegistrationStatus)
                .IsRequired();

            builder.HasOne(x => x.Address)
                .WithOne()
                .HasForeignKey<CompanyEntity>(x => x.AddressId);

            builder.HasOne(x => x.User)
                .WithOne()
                .HasForeignKey<CompanyEntity>(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
