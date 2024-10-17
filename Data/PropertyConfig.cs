using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dashboardManger.Models;


namespace dashboardManger.Data
{
    public class PropertyConfig : IEntityTypeConfiguration<RealEstateProperty>
    {
        public void Configure(EntityTypeBuilder<RealEstateProperty> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.GUID)
                .IsRequired()
                .HasDefaultValueSql("NEWID()")
                .HasMaxLength(36);

            builder.HasIndex(p => p.GUID)
            .IsUnique(); 

            builder.Property(p => p.Address)
                .IsRequired()
                .HasMaxLength(1024);
            builder.Property(p => p.StatusId)
                .IsRequired();

            builder.HasOne(p => p.ListingAgent1)
                .WithMany()
                .HasForeignKey(p => p.ListingAgent1Id)
                .OnDelete(DeleteBehavior.Restrict); 

            builder.HasOne(p => p.ListingAgent2)
                .WithMany()
                .HasForeignKey(p => p.ListingAgent2Id)
                .OnDelete(DeleteBehavior.Restrict);

            // Buyer 1 Information
            builder.Property(p => p.Buyer1FirstName)
                .HasMaxLength(50);

            builder.Property(p => p.Buyer1LastName)
                .HasMaxLength(50);

            builder.Property(p => p.Buyer1PhoneNumber)
                .HasMaxLength(15);

            builder.Property(p => p.Buyer1Email)
                .HasMaxLength(100);

            // Buyer 2 Information
            builder.Property(p => p.Buyer2FirstName)
                .HasMaxLength(50);

            builder.Property(p => p.Buyer2LastName)
                .HasMaxLength(50);

            builder.Property(p => p.Buyer2PhoneNumber)
                .HasMaxLength(15);

            builder.Property(p => p.Buyer2Email)
                .HasMaxLength(100);

            // Original Owner Information
            builder.Property(p => p.OriginalOwnerFirstName)
                .HasMaxLength(50);

            builder.Property(p => p.OriginalOwnerLastName)
                .HasMaxLength(50);

            builder.Property(p => p.OriginalOwnerPhoneNumber)
                .HasMaxLength(15);

            builder.Property(p => p.OriginalOwnerEmail)
                .HasMaxLength(100);

            builder.Property(p => p.ImageUrl)
                .HasMaxLength(2048);
    
            builder.Property(p => p.SoldPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");

            builder.Property(p => p.UpdatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
