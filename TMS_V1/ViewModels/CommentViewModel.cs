using System.ComponentModel.DataAnnotations;

namespace TMS_V1.ViewModels
{
    public class CommentViewModel
    {
        [StringLength(maximumLength:200,MinimumLength =5,ErrorMessage ="Please insert a description between 5 and 200 characters")]
        public string Description { get; set; }
        public string UserId { get; set; }
        public int TaskId { get; set; }
    }
}
