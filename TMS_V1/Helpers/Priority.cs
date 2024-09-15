using System.ComponentModel.DataAnnotations;

namespace TMS_V1.Helpers
{
    /// <summary>
    /// Represents the priority levels for tasks.
    /// </summary>
    public enum Priority
    {
        [Display(Name ="High")]
        HIGH,

        [Display(Name = "Medium")]
        MID,

        [Display(Name = "Easy")]
        EASY
    }
}
