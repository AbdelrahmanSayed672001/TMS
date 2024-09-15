
namespace TMS_V1.Configurations
{
    /// <summary>
    /// Configures the <see cref="Comment"/> entity and its properties and relationships in the database.
    /// </summary>
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.ToTable("Comments");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Description)
                   .IsRequired()
                   .HasMaxLength(200);

            builder.HasOne(c => c.User)
                   .WithMany(au => au.Comments)
                   .HasForeignKey(c => c.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(c => c.Task)
                   .WithMany(t => t.Comments)
                   .HasForeignKey(c => c.TaskId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
