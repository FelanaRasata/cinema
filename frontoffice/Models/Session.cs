using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace backoffice.Models
{
    public class Session
    {
        public int Id { get; set; }

        public int MovieId { get; set; }

        public Movie? Movie { get; set; }

        public DateTime SessionDateTime { get; set; }

        public int Room { get; set; }
    }
}
