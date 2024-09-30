using ECommerceAPI.Model;
using ECommerceAPI.Model.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ECommerceAPI.DataAccess
{
    public class ApplicationDbContext:IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        //public DbSet<ApplicationRoleManager> applicationRoleManagers { get; set; }
        //public DbSet<ApplicationRoleStore> applicationRoleStores { get; set; }
        //public DbSet<ApplicationUserManager> applicationUserManagers { get; set; }
        //public DbSet<ApplicationUserStore> applicationUserStores { get; set; }
        //public DbSet<ApplicationSignInManager> applicationSignInManagers { get; set; }
    }
}
