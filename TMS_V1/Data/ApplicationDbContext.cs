
using System.Reflection;

namespace TMS_V1.Data
{
    /// <summary>
    /// Represents the database context for the application, 
    /// inheriting from <see cref="IdentityDbContext{TUser}"/>.
    /// This context manages the database operations for user identity and other entities.
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<RegUser> RegUsers  { get; set; }
        public DbSet<Models.TaskEntity> Tasks   { get; set; }
        public DbSet<Team> Teams{ get; set; }
        public DbSet<TeamLead> TeamLeads { get; set; }
        public DbSet<UserTeam> UserTeams { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);


            // Map the identity-related tables to the 'Security' schema
            builder.Entity<ApplicationUser>().ToTable("Users", "Security");
            builder.Entity<IdentityRole>().ToTable("Roles", "Security");
            builder.Entity<IdentityUserRole<string>>().ToTable("UserRoles", "Security");
            builder.Entity<IdentityUserClaim<string>>().ToTable("UserClaims", "Security");
            builder.Entity<IdentityUserLogin<string>>().ToTable("UserLogins", "Security");
            builder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaims", "Security");
            builder.Entity<IdentityUserToken<string>>().ToTable("UserTokens", "Security");

            // Apply entity configurations from the executing assembly
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
