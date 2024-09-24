using EstudoMVC.DataContent.Enum;
using EstudoMVC.Models;
using EstudoMVC.Services;

namespace EstudoMVC.ViewModels
{
    public class AttractionViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public float ReviewsAvg { get; set; } = 0;
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

        public string ScoreAvg()
        {
            int roundedScore = (int)Math.Round(ReviewsAvg);

            if (Enum.IsDefined(typeof(Score), roundedScore))
            {
                // Retorna a descrição correspondente ao valor do enum
                return ((Score)roundedScore).GetDescription();
            }
            else
            {
                return "Erro! Sem avaliação";
            }

        }
    }
}
