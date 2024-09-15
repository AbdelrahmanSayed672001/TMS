
namespace TMS_V1.Configurations
{
    /// <summary>
    /// Configures the <see cref="TeamLead"/> entity and its properties and relationships in the database.
    /// </summary>
    public class TeamLeadConfiguration : IEntityTypeConfiguration<TeamLead>
    {
        public void Configure(EntityTypeBuilder<TeamLead> builder)
        {
            builder.ToTable("TeamLeads");

            builder.HasKey(tl => tl.Id);

            builder.HasOne(tl => tl.User)
                   .WithOne(au => au.TeamLead)
                   .HasForeignKey<TeamLead>(tl => tl.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            //builder.HasMany(tl => tl.User.UserTeams)
            //       .WithOne(ut => ut.User)
            //       .HasForeignKey(ut => ut.UserId);

            //builder.HasMany(tl => tl.Tasks)
            //       .WithOne(t => t.User)
            //       .HasForeignKey(t => t.UserId);

            //builder.HasMany(tl => tl.Comments)
            //       .WithOne(c => c.User)
            //       .HasForeignKey(c => c.UserId);
        }
    }

}
