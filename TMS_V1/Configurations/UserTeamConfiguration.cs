
namespace TMS_V1.Configurations
{
    /// <summary>
    /// Configures the <see cref="UserTeam"/> entity and its properties and relationships in the database.
    /// </summary>
    public class UserTeamConfiguration : IEntityTypeConfiguration<UserTeam>
    {
        public void Configure(EntityTypeBuilder<UserTeam> builder)
        {
            builder.ToTable("UserTeams");

            builder.HasKey(ut => new { ut.UserId, ut.TeamId });

            builder.HasOne(ut => ut.User)
                   .WithMany(au => au.UserTeams)
                   .HasForeignKey(ut => ut.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ut => ut.Team)
                   .WithMany(t => t.UserTeams)
                   .HasForeignKey(ut => ut.TeamId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
