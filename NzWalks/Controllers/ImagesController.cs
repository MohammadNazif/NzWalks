using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NzWalks.Migrations;
using NzWalks.Models.DomainModel;
using NzWalks.Models.DTOs;
using NzWalks.Repositories;

namespace NzWalks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagesController : ControllerBase
    {
        private readonly IimageRepo iimageRepo;

        public ImagesController(IimageRepo iimageRepo)
        {
            this.iimageRepo = iimageRepo;
        }
        //Post : /api/Images/Upload
        [HttpPost("Upload")]
         public async Task<IActionResult> Upload([FromForm] ImageUploadDto imageUploadDto )
        {
            ValidateFileUpload(imageUploadDto);   
            if(ModelState.IsValid)
            {
                //convert to domain model
                var imageDomainModel = new Image
                { 
                    File = imageUploadDto.File,
                    FileExtension = Path.GetExtension(imageUploadDto.File.FileName),
                    FileSizeInBytes = imageUploadDto.File.Length,
                    FileName = imageUploadDto.File.FileName,
                    FileDescription = imageUploadDto.FileDescription,
                };

                await iimageRepo.Upload(imageDomainModel);
                return Ok(imageDomainModel );
                
            }
            return BadRequest(ModelState);
        }
        private void ValidateFileUpload(ImageUploadDto imageUploadDto)
        {
            var allowedExtensions = new string[] { ".jpg", ".jpeg", ".png" };
            if (!allowedExtensions.Contains(Path.GetExtension(imageUploadDto.File.FileName)))
            {
                ModelState.AddModelError("file", "Unsupported file extension");
            }
            if(imageUploadDto.File.Length > 10485760)
            {
                ModelState.AddModelError("File", "File is more size");
            }

        }
    }
}
