using Amazon.S3;
using Amazon.S3.Model;
using EstudoMVC.Interfaces;

namespace EstudoMVC.Services
{
    public class TouristAttractionImageService : ITouristAttractionImageService
    {
        private readonly IAmazonS3 _amazonS3;
        private const string _bucketName = "touristattractionimages";

        public TouristAttractionImageService(IAmazonS3 amazonS3)
        {
            _amazonS3 = amazonS3 ?? throw new ArgumentNullException(nameof(amazonS3));
        }

        public async Task<PutObjectResponse> UploadImageAsync(Guid id, IFormFile file)
        {
            var putObjectRequest = new PutObjectRequest
            {
                BucketName = _bucketName,
                Key = $"profile_images{id}",
                ContentType = file.ContentType,
                InputStream = file.OpenReadStream(),
                Metadata =
                {
                    ["x-amz-meta-originalname"] = file.FileName ,
                    ["x-amz-meta-extension"] = Path.GetExtension(file.FileName),
                }
            };
            return await _amazonS3.PutObjectAsync(putObjectRequest);
        }

        public string GetBucketName() { return _bucketName; }
    }
}
