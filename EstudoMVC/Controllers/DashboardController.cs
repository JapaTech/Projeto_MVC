using EstudoMVC.DataContent;
using EstudoMVC.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EstudoMVC.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public DashboardController(IDashboardService dashboardService)
        {
            dashboardService = _dashboardService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
