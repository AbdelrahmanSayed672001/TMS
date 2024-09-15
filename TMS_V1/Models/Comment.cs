namespace TMS_V1.Models
{
    /// <summary>
    /// Represents a comment on a task, with a reference to 
    /// the user who posted it and the task it belongs to.
    /// </summary>
    public class Comment
    {
        public int Id { get; set; }
        public string Description { get; set; }

        public string UserId{ get; set; }
        public ApplicationUser User{ get; set; }

        public int TaskId { get; set; }
        public TaskEntity? Task { get; set; }

    }
}
