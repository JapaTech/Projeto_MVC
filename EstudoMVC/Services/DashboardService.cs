using EstudoMVC.DataContent;
using EstudoMVC.Interfaces;
using EstudoMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace EstudoMVC.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly MVC_DbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public DashboardService(MVC_DbContext context, IHttpContextAccessor httpContextAccessor) 
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<List<Review>> GetAllReviewsAsync()
        {
            var curUser = _httpContextAccessor.HttpContext?.User;
            var touristAttractions =  _context.Reviews.Where(x => x.UserId == curUser.ToString());
            return touristAttractions.ToList();
        }
    }
}
