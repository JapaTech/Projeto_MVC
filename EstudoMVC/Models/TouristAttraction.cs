using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstudoMVC.Models
{
    public class TouristAttraction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image {  get; set; }
        public float ReviewsAvg { get; set; } = 0;
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
