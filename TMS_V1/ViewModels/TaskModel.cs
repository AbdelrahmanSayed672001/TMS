using System.ComponentModel.DataAnnotations;

namespace TMS_V1.ViewModels
{
    public class TaskModel
    {
        [StringLength(70, MinimumLength = 3, ErrorMessage = "Please insert a title with min 3 and max 70")]
        public string Title { get; set; }


        [StringLength(200, MinimumLength = 5, ErrorMessage = "Please insert a title with min 5 and max 200")]

        public string Description { get; set; }

        public DateTime DueDate { get; set; }


        public Status? Status { get; set; }
        public Priority? Priority { get; set; }

        
    }
}
