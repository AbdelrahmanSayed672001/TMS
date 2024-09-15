using System.ComponentModel.DataAnnotations;

namespace TMS_V1.Helpers
{

    /// <summary>
    /// Represents the various statuses a task can have.
    /// </summary>
    public enum Status
    {
        [Display(Name ="To Do")]
        TODO, // 0

        [Display(Name = "In Progress")]
        IN_PROGRESS, // 1

        [Display(Name = "Completed")]
        COMPLETED // 2
    }
}
