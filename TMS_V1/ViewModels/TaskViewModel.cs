using System.ComponentModel.DataAnnotations;

namespace TMS_V1.ViewModels
{
    public class TaskViewModel : TaskModel
    {
        public int TeamId { get; set; }
        public IFormFile Attachment { get; set; }

    }
}
