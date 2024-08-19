﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dashboardManger.Models;

namespace dashboardManger.Data
{
    public class UserConfig : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(100)
                .HasAnnotation("EmailAddress", true);

            builder.Property(e => e.Guid)
                .IsRequired()
                .HasDefaultValueSql("NEWID()")
                .HasColumnType("uniqueidentifier");
        }
    }
}