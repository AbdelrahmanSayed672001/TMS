


using System.Drawing.Imaging;
using System.Drawing;
using System.Net.Mail;
using Microsoft.AspNetCore.Routing;
using static System.Net.Mime.MediaTypeNames;

namespace TMS_V1.Services
{
    /// <summary>
    /// Implementation of the <see cref="IAttachment"/> interface providing attachment-related services.
    /// </summary>
    public class AttachmentService : IAttachment
    {
        private readonly IWebHostEnvironment _environment;
        private const string PDF = "/Pdf";
        private const string IMAGES = "/Images";
        private Dictionary<string, string> extensionToDirectory = new Dictionary<string, string>{
            { ".pdf", PDF } };

        public AttachmentService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }


        /// <inheritdoc/>
        /// <remarks>
        /// Uploads an attachment to the specified root directory.
        /// </remarks>
        public async Task<string> UploadFile(IFormFile attachment, string root)
        {
            var fileName = string.Empty;
            var filePath = string.Empty;
            var UploadDir = string.Empty;

            if (!string.IsNullOrEmpty(attachment.FileName))
            {
                var extension = Path.GetExtension(attachment.FileName).ToLower();
                if (root == "Attachments")
                {
                    if (extensionToDirectory.TryGetValue(extension, out var directory))
                        UploadDir = Path.Combine(_environment.WebRootPath, root + directory);

                    else
                        UploadDir = Path.Combine(_environment.WebRootPath, root + IMAGES);
                }

                fileName = $"{Guid.NewGuid().ToString().Substring(0, 5)}_{attachment.FileName}";

                filePath = Path.Combine(UploadDir, fileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await attachment.CopyToAsync(fileStream);
                    await fileStream.DisposeAsync();
                }
            }
            return fileName;
        }

        /// <inheritdoc/>
        /// <remarks>
        /// Deletes an attachment from the specified root directory.
        /// </remarks>
        public void DeleteFile(string img, string root)
        {
            var filePath = string.Empty;    
            var extension = Path.GetExtension(img).ToLower();
            if (root == "Attachments")
            {
                if (extensionToDirectory.TryGetValue(extension, out var directory))
                    filePath = Path.Combine(_environment.WebRootPath, root + directory,img);

                else
                    filePath = Path.Combine(_environment.WebRootPath, root + IMAGES,img);
            }

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
        }

    }
}
