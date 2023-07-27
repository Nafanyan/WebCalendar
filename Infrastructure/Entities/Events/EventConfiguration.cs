using Application.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Entities.Events
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure( EntityTypeBuilder<Event> builder )
        {
            builder.HasKey( e => new { e.UserId, e.StartEvent, e.EndEvent } );

            builder.Property( e => e.UserId ).IsRequired();
            builder.Property( e => e.Name ).HasMaxLength( 100 ).IsRequired();
            builder.Property( e => e.Description ).HasMaxLength( 300 ).IsRequired();
            builder.Property( e => e.StartEvent ).IsRequired();
            builder.Property( e => e.EndEvent ).IsRequired();
        }
    }
}
