using Microsoft.AspNetCore.Routing;

namespace TMS_V1.Helpers
{
    /// <summary>
    /// Provides methods for validating files based on extension and size.
    /// </summary>
    public class FileValidation
    {
        public static readonly List<string> Extensions = new List<string> { ".png", ".jpg", ".jpeg",".pdf" };
        public static readonly int MaxSize = 1048576; // 1MB


        /// <summary>
        /// Checks if the file extension is allowed.
        /// </summary>
        /// <param name="file">The file to validate.</param>
        /// <returns><c>true</c> if the extension is allowed; otherwise, <c>false</c>.</returns>
        public static bool IsExtensionAllowed(IFormFile file)
        {
            if ((Extensions.Contains(Path.GetExtension(file.FileName).ToLower())))
            {
                return true;
            }
            return false;
        }


        /// <summary>
        /// Checks if the file size is within the allowed limit.
        /// </summary>
        /// <param name="file">The file to validate.</param>
        /// <returns><c>true</c> if the file size is allowed; otherwise, <c>false</c>.</returns>
        public static bool IsSizeAllowed(IFormFile file)
        {
            if (file.Length < MaxSize)
            {
                return true;
            }
            return false;
        }

        

    }
}
