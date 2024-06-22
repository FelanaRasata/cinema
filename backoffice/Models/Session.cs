using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backoffice.Models
{
    public class Session
    {
        [Key] // Specifies that Id is the primary key
        public int Id { get; set; }

        [Required]
        public int MovieId { get; set; }

        [ForeignKey("MovieId")] // Specifies the foreign key relationship
        public Movie? Movie { get; set; }

        [Required]
        public DateTime SessionDateTime { get; set; }

        [Required]
        public int Room { get; set; }
    }
}
