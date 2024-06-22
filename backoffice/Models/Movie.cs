using System.ComponentModel.DataAnnotations;

namespace backoffice.Models
{
    public class Movie
    {
        [Key] // Specifies that Id is the primary key
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [StringLength(1000)]
        public string Description { get; set; }

        [Required]
        public int Duration { get; set; }

        [StringLength(50)]
        public string Category { get; set; }
    }
}
