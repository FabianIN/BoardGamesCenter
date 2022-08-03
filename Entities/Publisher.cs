using System.ComponentModel.DataAnnotations;

namespace BoardGamesCenter.Entities
{    public class Publisher
    {
        [Key]

        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        [MaxLength(2500)]
        public string CDescription { get; set; }

        public bool? Deleted { get; set; }
    }
}

