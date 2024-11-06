using EstudoMVC.DataContent;
using EstudoMVC.Interfaces;
using EstudoMVC.Models;

namespace EstudoMVC.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly MVC_DbContext _context;
        private readonly IHttpContextAccessor _contextAccessor;

        public DashboardService(MVC_DbContext context, IHttpContextAccessor contextAccessor) 
        {
            _context = context;
            _contextAccessor = contextAccessor;
        }

        public async Task<User> GetUserById(string id)
        {
            return await _context.Users.FindAsync(id);
        }


    }
}
