namespace TMS_V1.Models
{
    /// <summary>
    /// Represents the many-to-many relationship between <see cref="ApplicationUser"/> and <see cref="Team"/>.
    /// </summary>
    public class UserTeam
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public int TeamId { get; set; }
        public Team Team { get; set; }
    }
}
