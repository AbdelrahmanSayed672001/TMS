namespace TMS_V1.Models
{
    /// <summary>
    /// Represents a regular user, including a reference 
    /// to the <see cref="ApplicationUser"/> it is associated with.
    /// </summary>
    public class RegUser
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        //public ICollection<UserTeam> UserTeams { get; set; }
        //public ICollection<Task>? Tasks { get; set; }
        //public ICollection<Comment>? Comments { get; set; }

    }
}
