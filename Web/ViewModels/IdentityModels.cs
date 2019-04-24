using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Web.DataModels;

namespace Web.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, 
    // please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
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
        public DbSet<MemberIdentityDataModel> MemberIdentities { get; set; }
        public DbSet<MemberRoleDataModel> MemberRoles { get; set; }
        public DbSet<ExpressInterestDataModel> ExpressionsOfInterest { get; set; }
        public DbSet<NumberAssignmentDataModel> NumberAssignments { get; set; }
        public DbSet<TrialAssessmentDataModel> TrialAssessments { get; set; }
        public DbSet<MemberRegistrationDataModel> Registrations { get; set; }
        public DbSet<MemberLegalDataModel> Consents { get; set; }
        public DbSet<UmpireDataModel> Umpires { get; set; }
        public DbSet<PlayingHistoryDataModel> PlayingHistories { get; set; }
        public DbSet<PlayingShirtDataModel> PlayingShirts { get; set; }
        public DbSet<ContactDataModel> ContactSubmissions { get; set; }

        public ApplicationDbContext() : base("LocalConnection")
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}