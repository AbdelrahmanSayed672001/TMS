
using Microsoft.VisualBasic;

namespace TMS_V1.Models
{
    /// <summary>
    /// Represents an application user that extends the <see cref="IdentityUser"/> class.
    /// Includes relationships to team leads, regular users, tasks, user teams, and comments.
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        public TeamLead TeamLead { get; set; }
        public RegUser RegUser { get; set; }
        public ICollection<TaskEntity>? Tasks { get; set; }
        public ICollection<UserTeam>? UserTeams { get; set; }
        public ICollection<Comment>? Comments { get; set; }

    }
}
