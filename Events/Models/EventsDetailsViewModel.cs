using Events.Models;
using System.ComponentModel.DataAnnotations;

namespace Events.Models
{
    public class EventsDetailsViewModel
    {
        public AddEvents Events { get; set; }
        public List<PrivateParticipants> privateParticipants { get; set; }
        public List<CompanyParticipants> companyParticipants { get; set; }
        public int EventsId { get; set; }
        [StringLength(64, MinimumLength = 3)]
        [Required]
        public string Eesnimi { get; set; }
        [StringLength(64, MinimumLength = 3)]
        [Required]
        public string Perekonnanimi { get; set; }
        [Required]
        public int Isikukood { get; set; }
        [Required]
        public string Maksmisviis { get; set; }
        [StringLength(1500)]
        public string Lisainfo { get; set; }
    }
}
