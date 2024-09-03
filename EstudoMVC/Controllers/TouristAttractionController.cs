using EstudoMVC.DataContent;
using EstudoMVC.Interfaces;
using EstudoMVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EstudoMVC.Controllers
{
    public class TouristAttractionController : Controller
    {
        private readonly MVC_DbContext _context;
        private readonly ITouristAttractionService _touristAttractionService;
        private readonly ITouristAttractionImageService _imageService;

        public TouristAttractionController(MVC_DbContext context, ITouristAttractionService touristAttractionService,
            ITouristAttractionImageService imageService)
        {
            _context = context;
            _touristAttractionService = touristAttractionService;
            _imageService = imageService;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<TouristAttraction> touristAttractions = await _touristAttractionService.GetAll();
            return View(touristAttractions);
        }

        
        public async Task<IActionResult> Detail(int id)
        {
            TouristAttraction attraction = await _touristAttractionService.GetByIdAsync(id);
            return View(attraction);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(TouristAttraction touristAttraction, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return View(touristAttraction);
            }
            

            if (image != null && image.Length > 0) 
            {
                Guid imagemGuid = new Guid();

                var response = await _imageService.UploadImageAsync(imagemGuid, image);

                if (response.HttpStatusCode != HttpStatusCode.OK)
                {
                    ModelState.AddModelError("", "Falha ao carregar a imagem.");
                    return View(touristAttraction);
                }
                touristAttraction.Image = $"https://touristattractionimages.s3.amazonaws.com/profile_images{imagemGuid}";
            }

            _touristAttractionService.Add(touristAttraction);

            return RedirectToAction(nameof(Index));
        }
    }
}
