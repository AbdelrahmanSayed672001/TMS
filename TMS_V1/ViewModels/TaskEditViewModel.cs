namespace TMS_V1.ViewModels
{
    public class TaskEditViewModel : TaskModel
    {
        public int Id { get; set; }
        public IFormFile? Attachment { get; set; }

        public string? AttachmentName { get; set; }
        public string? UserId{ get; set; }

        public int? TeamId { get; set; }
    }
}
