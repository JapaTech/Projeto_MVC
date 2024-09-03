using Amazon.S3.Model;

namespace EstudoMVC.Interfaces
{
    public interface ITouristAttractionImageService
    {
        Task<PutObjectResponse> UploadImageAsync(Guid id, IFormFile file);
    }
}
