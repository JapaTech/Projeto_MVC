using EstudoMVC.DataContent;
using EstudoMVC.Interfaces;
using EstudoMVC.Models;
using EstudoMVC.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace EstudoMVC.Services
{
    public class ReviewService : IReviewService
    {
        public readonly MVC_DbContext _context;

        public ReviewService(MVC_DbContext context)
        {
            _context = context;
        }

        public bool Add(Review item)
        {
            _context.Add(item);
            return Save();
        }

        public bool Delete(Review item)
        {
            _context.Remove(item);
            return Save();
        }

        public async Task<IEnumerable<Review>> GetAll()
        {
            return await _context.Reviews.ToListAsync();
        }

        public async Task<IEnumerable<Review>> GetAllById(string id)
        {
            return await _context.Reviews.Where(r => r.UserId == id).Include(r => r.User).Include(r=>r.TouristAttraction).ToListAsync();
        }

        public async Task<Review> GetByIdAsync(int id)
        {
            return await _context.Reviews.FirstOrDefaultAsync(x => x.Id == id) ;
        }

        public async Task<Review> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Reviews.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Review item)
        {
            _context.Update(item);
            return Save();
        }

        public async Task<bool> UpdateReviewAsync(int id, ReviewViewModel reviewVM)
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null) return false;

            review.Content = reviewVM.Content;
            review.MainExperience = reviewVM.MainExperience;
            review.SideExperience = reviewVM.SideExperience;
            review.Score = reviewVM.Score;

            await _context.SaveChangesAsync();
            return true;
        }
    }
}
