using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PresentationLayer.Models.dbModels;

namespace PresentationLayer.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Championship>().HasMany(o => o.News).WithOptional(o => o.Championship).HasForeignKey(o => o.ChampionshipId);
            modelBuilder.Entity<Match>().HasMany(o => o.News).WithOptional(o => o.Match).HasForeignKey(o => o.MatchId);
            modelBuilder.Entity<News>().HasMany(o => o.Albums).WithOptional(o => o.News).HasForeignKey(o => o.NewsId);
            modelBuilder.Entity<Match>().HasMany(o => o.Albums).WithOptional(o => o.Match).HasForeignKey(o => o.MatchId);
            modelBuilder.Entity<Match>().HasMany(o => o.Goals).WithRequired(o => o.Match).HasForeignKey(o => o.MatchId);
            modelBuilder.Entity<Album>().HasMany(o => o.Attachments).WithRequired(o => o.Album).HasForeignKey(o => o.AlbumId);

        }

        public DbSet<Championship> Championships { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Match> Matchs { get; set; }
        public DbSet<Album> Albums { get; set; }
        public DbSet<Goal> Goals { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}