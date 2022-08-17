using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Events.Models;

namespace Events.Data
{
    public class EventsContext : DbContext
    {
        public EventsContext (DbContextOptions<EventsContext> options)
            : base(options)
        {
        }

        public DbSet<Events.Models.AddEvents> AddEvents { get; set; } = default!;
    }
}
