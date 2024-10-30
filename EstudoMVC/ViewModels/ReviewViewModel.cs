using EstudoMVC.DataContent.Enum;
using EstudoMVC.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EstudoMVC.ViewModels
{
    public class ReviewViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }
        
        [Required]
        public DateTime CreationDate { get; set; } = DateTime.Now;
        
        [Required]
        public ExperienceType MainExperience { get; set; }
        public ExperienceType? SideExperience { get; set; }         
        [Required]
        public Score Score { get; set; }

    }
}
