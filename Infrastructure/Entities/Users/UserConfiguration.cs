using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Entities.Users
{
    internal class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure( EntityTypeBuilder<User> builder )
        {
            builder.ToTable( "Users" )
                .HasKey( u => u.Id );

            builder.Property( u => u.Id ).ValueGeneratedOnAdd();
            builder.Property( u => u.Login ).HasMaxLength( 28 ).IsRequired();
            builder.Property( u => u.PasswordHash ).HasMaxLength( 100 ).IsRequired();

            builder.HasMany<Event>()
                .WithOne()
                .HasForeignKey( e => e.UserId )
                .IsRequired();

            builder.HasOne<UserAuthorizationToken>()
                .WithOne()
                .HasForeignKey<UserAuthorizationToken>( ua => ua.UserId )
                .IsRequired();
        }
    }
}
