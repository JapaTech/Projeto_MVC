using System.ComponentModel.DataAnnotations;

namespace EstudoMVC.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public DateTime Birth { get; set; }

        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public void AddReview(Review review)
        {
            Reviews.Add(review);
        }

        public void UpdateReview(Review review, string newText) 
        { 
            review.Content = newText;
            review.CreationDate = DateTime.Now;
        }
    }
}
    