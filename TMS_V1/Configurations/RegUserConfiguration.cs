
namespace TMS_V1.Configurations
{
    /// <summary>
    /// Configures the <see cref="RegUser"/> entity and its properties and relationships in the database.
    /// </summary>
    public class RegUserConfiguration : IEntityTypeConfiguration<RegUser>
    {
        public void Configure(EntityTypeBuilder<RegUser> builder)
        {
            builder.ToTable("RegUsers");

            builder.HasKey(ru => ru.Id);

            builder.HasOne(ru => ru.User)
                   .WithOne(au => au.RegUser)
                   .HasForeignKey<RegUser>(ru => ru.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            //builder.HasMany(ru => ru.Teams)
            //       .WithOne(ut => ut.User)
            //       .HasForeignKey(ut => ut.UserId);

            //builder.HasMany(ru => ru.Tasks)
            //       .WithOne(t => t.User)
            //       .HasForeignKey(t => t.UserId);

            //builder.HasMany(ru => ru.Comments)
            //       .WithOne(c => c.User)
            //       .HasForeignKey(c => c.UserId);
        }
    }

}
