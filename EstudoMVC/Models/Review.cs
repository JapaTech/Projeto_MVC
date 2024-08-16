using EstudoMVC.DataContent.Enum;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EstudoMVC.Models
{
    public class Review
    {
        [Key]
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public ExperienceType MainExperience { get; set; }
        public ExperienceType? SideExperience { get; set; } = null;
        public Score Score { get; set; }
        
        [ForeignKey("TouristAttraction")]
        public int TouristAttractionId { get; set; }
        public TouristAttraction TouristAttraction { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public User User { get; set; }

        public Review() { }

        public Review(int id, string content, DateTime reviewDate, 
            ExperienceType mainExperience, ExperienceType? sideExperience, Score score, 
            int touristAttractionId, TouristAttraction touristAttraction, int userId, User user)
        {
            Id = id;
            Content = content;
            CreationDate = reviewDate;
            MainExperience = mainExperience;
            SideExperience = sideExperience;
            Score = score;
            TouristAttractionId = touristAttractionId;
            TouristAttraction = touristAttraction;
            UserId = userId;
            User = user;
        }
    }
}
