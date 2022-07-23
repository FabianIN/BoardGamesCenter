using System.ComponentModel.DataAnnotations;

namespace BoardGamesCenter.Entities
{    public class Publisher
    {
        [Key]

        public Guid ID { get; set; }
        [Required]
        [MaxLength(150)]
        public string CompanyName { get; set; }

        [MaxLength(2500)]
        public string CompanyDescription { get; set; }

        public bool? Deleted { get; set; }
    }
}
}
