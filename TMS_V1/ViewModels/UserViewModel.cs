using System.ComponentModel.DataAnnotations;

namespace TMS_V1.ViewModels
{
    public class UserViewModel : InputRegisterViewModel
    {
        public List<int> TeamsId { get; set; }
    }

}
