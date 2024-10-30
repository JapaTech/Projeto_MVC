using EstudoMVC.Models;
using EstudoMVC.ViewModels;

namespace EstudoMVC.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAll();
        Task<IEnumerable<Review>> GetAllById(string id);
        Task<Review> GetByIdAsync(int id);
        Task<Review> GetByIdAsyncNoTracking(int id);
        bool Add(Review item);
        bool Delete(Review item);
        bool Update(Review item);
        bool Save();
        Task<bool> UpdateReviewAsync(int id, ReviewViewModel reviewVM);
    }
}
