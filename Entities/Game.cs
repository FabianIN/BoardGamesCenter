using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoardGamesCenter.Entities
{
    public class Game
    {
        [Key]

        public Guid Id { get; set; }

        [Required]
        [MaxLength(150)]

        public string Title { get; set; }

        [MaxLength(2500)]

        public string Description { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        [ForeignKey("AuthorId")]
        public virtual Author Author { get; set; }

        [Required]
        public Guid PublisherId { get; set; }

        [ForeignKey("PublisherId")]
        public virtual Publisher Publisher { get; set; }

        public bool? Deleted { get; set; }
    }
}
