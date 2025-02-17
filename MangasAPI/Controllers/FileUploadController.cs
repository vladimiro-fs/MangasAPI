namespace MangasAPI.Controllers
{
    using MangasAPI.Entities;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Route("api/[controller]")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        private readonly IWebHostEnvironment _environment;
        private readonly ILogger<FileUploadController> _logger;

        public FileUploadController(IWebHostEnvironment environment, 
                                    ILogger<FileUploadController> logger)
        {
            _environment = environment;
            _logger = logger;
        }

        [HttpPost("upload")]
        public async Task<ActionResult<IList<UploadResult>>> PostFile(
                                       [FromForm] IEnumerable<IFormFile> files)
        {
            var maxFileNumber = 3;
            long maxFileSize = 1024 * 100;
            var processedFiles = 0;
            var resourcePath = new Uri($"{Request.Scheme}: //{Request.Host}/");
            List<UploadResult> uploadsResult = new();

            foreach(var file in files)
            {
                if (!VerifyFileExtension(file))
                {
                    return BadRequest($"The file doesn't have an extension or it's not an image." + 
                        $"Supported extensions: .jpg/ .bmp/ .png");
                }

                var uploadResult = new UploadResult();
                uploadResult.FileName = file.FileName;

                if (processedFiles < maxFileNumber)
                {
                    if (file.Length == 0)
                    {
                        _logger.LogInformation("{FileName} size is 0 (Err: 1)", file.FileName);
                        uploadResult.ErrorCode = 1;
                    }
                    else if (file.Length > maxFileSize)
                    {
                        _logger.LogInformation("{FileName} of {Length} bytes is " +
                                               "bigger than the limit of {Limit} bytes (Err 2)", 
                                               file.FileName, file.Length, maxFileSize);
                        uploadResult.ErrorCode = 2;
                    }
                    else
                    {
                        try
                        {
                            var path = Path.Combine(_environment.WebRootPath, "images", file.FileName);

                            await using FileStream fs = new (path, FileMode.Create);
                            await file.CopyToAsync(fs);

                            _logger.LogInformation("{FileName} saved on {Path}", 
                                                   file.FileName, path);

                            uploadResult.Uploaded = true;
                        }
                        catch (IOException ex)
                        {
                            _logger.LogError("{FileName} error sending (Err 3): {Message}", 
                                             file.FileName, ex.Message);
                            uploadResult.ErrorCode = 3;
                        }
                    }

                    processedFiles++;
                }
                else
                {
                    _logger.LogInformation("{FileName} not sent since request" +
                                            "exceeded {Count} files (Err 4)", 
                                            file.FileName, maxFileNumber);
                    uploadResult.ErrorCode = 4;
                }

                uploadsResult.Add(uploadResult);
            }

            return new CreatedResult(resourcePath, uploadsResult);
        }

        private bool VerifyFileExtension(IFormFile file)
        {
            string[] extensions = new string[] { "jpg", "bmp", "png" };

            var extensionFileName = file.FileName.Split('.')[1];

            if (string.IsNullOrEmpty(extensionFileName) ||
                !extensions.Contains(extensionFileName))
            {
                return false;
            }

            return true;
        }
    }
}
