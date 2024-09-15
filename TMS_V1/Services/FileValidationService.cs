namespace TMS_V1.Services
{
    public class FileValidationService : IFileValidationService
    {
        public (bool IsValid, string Message) ValidateFile(IFormFile file)
        {
            if (!FileValidation.IsExtensionAllowed(file))
            {
                return (false, "Extension not allowed");
            }

            if (!FileValidation.IsSizeAllowed(file))
            {
                return (false, "Size is big, max size is 1MB");
            }

            return (true, null);
        }
    }
}
