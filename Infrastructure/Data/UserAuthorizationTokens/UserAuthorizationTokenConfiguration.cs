﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.UserAuthorizationTokens
{
    internal class UserAuthorizationTokenConfiguration : IEntityTypeConfiguration<UserAuthorizationToken>
    {
        public void Configure(EntityTypeBuilder<UserAuthorizationToken> builder)
        {
            builder.ToTable("UserAuthorizationTokens")
                .HasKey(ua => ua.UserId);

            builder.Property(ua => ua.UserId).IsRequired();
            builder.Property(ua => ua.RefreshToken).IsRequired().HasMaxLength(40);
            builder.Property(ua => ua.ExpiryDate).IsRequired();
        }
    }
}
