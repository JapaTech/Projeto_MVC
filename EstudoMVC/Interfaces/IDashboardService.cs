using EstudoMVC.Models;

namespace EstudoMVC.Interfaces
{
    public interface IDashboardService
    {
        Task<List<Review>> GetAllReviewsAsync();
    }
}
