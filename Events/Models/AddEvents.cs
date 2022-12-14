using System.ComponentModel.DataAnnotations;

namespace Events.Models
{
    public class AddEvents
    {
        public int Id { get; set; }
        [StringLength(64, MinimumLength = 3)]
        [Required]
        [Display(Name = "Ürituse nimi")]
        public string Nimi { get; set; }
        [Required(ErrorMessage = "Palun sisesta kuupäev, formaat: pp.kk.aaaa hh:mm")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm}")]
        public DateTime Toimumisaeg { get; set; }
        [StringLength(64)]
        [Required]
        public string Koht { get; set; }
        [StringLength(1000)]
        public string? Lisainfo { get; set; }
    }
}
