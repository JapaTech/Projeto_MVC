using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstudoMVC.Models
{
    public class User : IdentityUser
    {
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
    