using EstudoMVC.Models;

namespace EstudoMVC.Interfaces
{
    public interface IDashboardService
    {
        public Task<User> GetUserById(string id);
    }
}
