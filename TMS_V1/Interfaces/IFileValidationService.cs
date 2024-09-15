
namespace TMS_V1.Interfaces
{
    public interface IFileValidationService
    {
        (bool IsValid, string Message) ValidateFile(IFormFile file);
    }
}
