using System.ComponentModel.DataAnnotations;

namespace TMS_V1.ViewModels
{
    public class TeamViewModel
    {
        [StringLength(maximumLength:50, MinimumLength = 2, ErrorMessage ="Please insert valid Name betwenn 2 and 50 characters")]
        public string Name { get; set; }
    }
}
