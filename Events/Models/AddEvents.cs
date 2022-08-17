using System.ComponentModel.DataAnnotations;

namespace Events.Models
{
    public class AddEvents
    {
        [Key]
        public int Id { get; set; }
        [StringLength(64, MinimumLength = 3)]
        [Required]
        public string Nimi { get; set; }
        [Required(ErrorMessage = "Palun sisesta kuupäev, formaat: pp.kk.aaaa hh:mm")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy hh:mm}")]
        public DateTime Toimumisaeg { get; set; }
        [StringLength(64)]
        [Required]
        public string Koht { get; set; }
        [StringLength(1000)]
        public string lisainfo { get; set; }
    }
}
