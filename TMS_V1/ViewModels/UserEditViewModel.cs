using System.ComponentModel.DataAnnotations;

namespace TMS_V1.ViewModels
{
    public class UserEditViewModel 
    {
        public int Id { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Old password")]
        public string? OldPassword { get; set; }
        
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "New password")]
        public string? NewPassword { get; set; }

        
        [DataType(DataType.Password)]
        [Display(Name = "Confirm new password")]
        [Compare("NewPassword", ErrorMessage = "The password and confirmation password do not match.")]
        public string? ConfirmNewPassword { get; set; }
        public List<int> TeamsId{ get; set; }
        public ICollection<UserTeam>? UserTeams{ get; set; }
    }


    public class RegUserEditViewModel: UserEditViewModel
    {
        public ICollection<TaskEntity>? Tasks{ get; set; }

        public List<int>? TasksId { get; set; }
    }
}
