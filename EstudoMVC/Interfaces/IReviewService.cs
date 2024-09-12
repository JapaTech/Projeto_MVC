using EstudoMVC.Models;

namespace EstudoMVC.Interfaces
{
    public interface IReviewService
    {
        Task<IEnumerable<Review>> GetAll();
        Task<Review> GetByIdAsync(int id);
        Task<Review> GetByIdAsyncNoTracking(int id);
        bool Add(Review item);
        bool Delete(Review item);
        bool Update(Review item);
        bool Save();
    }
}
