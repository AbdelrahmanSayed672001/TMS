
namespace TMS_V1.Configurations
{
    /// <summary>
    /// Configures the <see cref="TaskEntity"/> entity and its properties and relationships in the database.
    /// </summary>
    public class TaskConfiguration : IEntityTypeConfiguration<TaskEntity>
    {
        public void Configure(EntityTypeBuilder<TaskEntity> builder)
        {
            builder.ToTable("Tasks");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Title)
                .IsRequired()
                .HasMaxLength(70);

            builder.Property(t => t.Description)
                .HasMaxLength(200);

            builder.Property(t => t.Attachment)
                .HasMaxLength(50);

            builder.Property(t => t.DueDate)
                .IsRequired();


            builder.HasOne(t => t.User)
                .WithMany(u => u.Tasks)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.SetNull);


            builder.HasOne(t => t.Team)
                .WithMany(t => t.Tasks)
                .HasForeignKey(t => t.TeamId)
                .OnDelete(DeleteBehavior.SetNull); ;


            builder.HasMany(t => t.Comments)
                .WithOne(c => c.Task)
                .HasForeignKey(t => t.TaskId);
        }

    }

}
