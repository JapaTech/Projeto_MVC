using System.ComponentModel.DataAnnotations;

namespace EstudoMVC.Models
{
    public class TouristAttraction
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Image {  get; set; }
        public float ReviewsAvg { get; set; }
        public ICollection<Review> Reviews { get; set; } = new List<Review>();
    }
}
