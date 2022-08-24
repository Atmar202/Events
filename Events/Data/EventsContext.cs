using Microsoft.EntityFrameworkCore;

namespace Events.Data
{
    public class EventsContext : DbContext
    {
        public EventsContext(DbContextOptions<EventsContext> options)
            : base(options)
        {
        }

        public DbSet<Events.Models.AddEvents> AddEvents { get; set; } = default!;

        public DbSet<Events.Models.PrivateParticipants>? PrivateParticipants { get; set; }
        public DbSet<Events.Models.CompanyParticipants>? CompanyParticipants { get; set; }
    }
}
