using EstudoMVC.Models;
using System.Collections;

namespace EstudoMVC.ViewModels
{
    public class TouristAttractionReviewViewModel
    {
        //public int TouristAttractionId { get; set; }
        public TouristAttraction? Attraction { get; set; } = null;

        public ReviewViewModel Review { get; set;}
        public ICollection <Review> Reviews { get; set;} = new List<Review>();
    }
}
