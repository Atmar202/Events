using Events.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Events.Models
{
    public class EventsDetailsViewModel
    {
        public AddEvents? Events { get; set; }
        public IEnumerable<PrivateParticipants>? privateParticipants { get; set; }
        public IEnumerable<CompanyParticipants>? companyParticipants { get; set; }
        public int EventsId { get; set; }
        public PrivateParticipants? privateParticipantsModel { get; set; }
        public CompanyParticipants? companyParticipantsModel { get; set; }
    }
}
