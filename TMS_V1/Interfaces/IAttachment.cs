
namespace TMS_V1.Interfaces
{
    public interface IAttachment
    {
        /// <summary>
        /// Uploads an attachment to the specified root directory.
        /// </summary>
        /// <param name="model">The attachment to upload.</param>
        /// <param name="root">The root directory where the file will be uploaded.</param>
        /// <returns>A task representing the asynchronous operation, 
        /// returning the path of the uploaded file.
        /// </returns>
        Task<string> UploadFile(IFormFile model, string root);


        /// <summary>
        /// Deletes an attachment from the specified root directory.
        /// </summary>
        /// <param name="model">The name or path of the attachment to delete.</param>
        /// <param name="root">The root directory from which the file will be deleted.</param>
        void DeleteFile(string model, string root);
    }
}
