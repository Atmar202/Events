using System.ComponentModel.DataAnnotations;

namespace Events.Models
{
        public class PrivateParticipants
        {
            [Key]
            public int PrivateId { get; set; }
            [StringLength(64, MinimumLength = 3)]
            [Required]
            public string Eesnimi { get; set; }
            [Required]
            public string Perekonnanimi { get; set; }
            [Required]
            public int Isikukood { get; set; }
            [Required]
            public string Maksmisviis { get; set; }
            [StringLength(1500)]
            public string? Lisainfo { get; set; }
            public virtual AddEvents? Events { get; set; }
        }

        public class CompanyParticipants
        {
            [Key]
            public int CompanyId { get; set; }
            [StringLength(64, MinimumLength = 3)]
            [Required]
            [Display(Name = "Juriidiline nimi")]
            public string Nimi { get; set; }
            [Required]
            public int Registrikood { get; set; }
            [Required]
            [Display(Name = "Osavõtjate arv")]
            public int Osavõtjate_arv { get; set; }
            [Required]
            public string Maksmiseviis { get; set; }
            [StringLength(5000)]
            public string? Lisainfo { get; set; }
            public virtual AddEvents? Events { get; set; }
        }
}
