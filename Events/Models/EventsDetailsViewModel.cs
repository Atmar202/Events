namespace Events.Models
{
    public class EventsDetailsViewModel
    {
        public AddEvents? Events { get; set; }
        public List<PrivateParticipants>? privateParticipants { get; set; }
        public List<CompanyParticipants>? companyParticipants { get; set; }
        public int EventsId { get; set; }
        public PrivateParticipants? privateParticipantsModel { get; set; }
        public CompanyParticipants? companyParticipantsModel { get; set; }
    }
}
