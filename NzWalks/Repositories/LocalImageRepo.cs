using NzWalks.Models.DomainModel;

namespace NzWalks.Repositories
{
    public class LocalImageRepo : IimageRepo
    {
       
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly DatabaseContext databaseContext;

        public LocalImageRepo(IWebHostEnvironment webHostEnvironment,IHttpContextAccessor httpContextAccessor,DatabaseContext databaseContext
            )
        {
            this.webHostEnvironment = webHostEnvironment;
            this.httpContextAccessor = httpContextAccessor;
            this.databaseContext = databaseContext;
        }

        public async Task<Image> Upload(Image image)
        {
            var localFilePath = Path.Combine(webHostEnvironment.ContentRootPath, "Images", $"{image.FileName}{image.FileExtension}");
            using var stream = new FileStream(localFilePath, FileMode.Create);
            await image.File.CopyToAsync(stream);


            var urlFilePath = $"{httpContextAccessor.HttpContext.Request.Scheme}" +
                $"://{httpContextAccessor.HttpContext.Request.Host}{httpContextAccessor.HttpContext.Request.PathBase}" +
                $"/Images/{image.FileName}{image.FileExtension}";

              image.FilePath = urlFilePath;
            await databaseContext.images.AddAsync(image);
            await databaseContext.SaveChangesAsync();
            return image;
        }
    }
}
