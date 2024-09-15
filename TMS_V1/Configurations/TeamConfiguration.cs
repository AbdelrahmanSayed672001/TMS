
namespace TMS_V1.Configurations
{
    /// <summary>
    /// Configures the <see cref="Team"/> entity and its properties and relationships in the database.
    /// </summary>
    public class TeamConfiguration : IEntityTypeConfiguration<Team>
    {
        public void Configure(EntityTypeBuilder<Team> builder)
        {
            builder.ToTable("Teams");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Name)
                .IsRequired()
                .HasMaxLength(50);


            //builder.HasMany(t => t.Tasks)
            //    .WithOne(task => task.Team)
            //    .HasForeignKey(task => task.TeamId);

            //builder.HasMany(t => t.UserTeams)
            //    .WithOne(ut => ut.Team)
            //    .HasForeignKey(ut => ut.TeamId);
        }

    }

}
