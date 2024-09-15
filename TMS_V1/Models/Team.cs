namespace TMS_V1.Models
{
    /// <summary>
    /// Represents a team, including its name and the tasks and user teams associated with it.
    /// </summary>
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<TaskEntity>? Tasks { get; set; }
        public ICollection<UserTeam>? UserTeams { get; set; }
    }
}
