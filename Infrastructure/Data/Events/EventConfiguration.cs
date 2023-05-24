using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Events
{
    internal class EventConfiguration : IEntityTypeConfiguration<Event>
    {
        public void Configure(EntityTypeBuilder<Event> builder)
        {

            builder.HasKey(e => new { e.UserId, e.EventPeriod});

            builder.OwnsOne(e => e.EventPeriod, ep =>
            {
                ep.Property(se => se.StartEvent).IsRequired();
                ep.Property(ee => ee.EndEvent).IsRequired();
            });

            builder.Property(e => e.UserId).IsRequired();
            builder.Property(e => e.Name).HasMaxLength(100).IsRequired();
            builder.Property(e => e.Description).HasMaxLength(200).IsRequired();
        }
    }
}
