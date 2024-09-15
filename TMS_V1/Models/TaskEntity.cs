using System.ComponentModel.DataAnnotations;

namespace TMS_V1.Models
{
    /// <summary>
    /// Represents a task, including details like 
    /// title, description, due date, status, priority, and associated user and team.
    /// </summary>
    public class TaskEntity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Attachment { get; set; }
        public DateTime DueDate { get; set; }
        public Status? Status { get; set; }
        public Priority? Priority { get; set; }

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public int? TeamId { get; set; }
        public Team? Team { get; set; }

        public ICollection<Comment>? Comments { get; set; }
    }
}
