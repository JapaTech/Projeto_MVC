using EstudoMVC.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EstudoMVC.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttractionImageController : ControllerBase
    {
        private readonly ITouristAttractionImageService _imageService;

        public AttractionImageController(ITouristAttractionImageService imageService)
        {
            _imageService = imageService;
        }

        [HttpPost("attractiom/id/image")]
        public async Task<IActionResult> Upload([FromRoute] Guid id,
            [FromForm(Name = "Data")] IFormFile file)
        {
            var response = await _imageService.UploadImageAsync(id, file);

            if (response.HttpStatusCode == HttpStatusCode.OK)
            {
                return Ok();

            }
            else
            {
                return BadRequest();
            }
        }
    }
}
