using System.ComponentModel.DataAnnotations;

namespace BoardGamesCenter.Entities
{
    public class Author
    {
        [Key]

        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(150)]
        public string LastName { get; set; }

        public bool? Deleted { get; set; }
    }
}
