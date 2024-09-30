using ECommerceAPI.DataAccess;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ECommerceAPI.Model.Identity
{
    public class ApplicationUserStore:UserStore<ApplicationUser>
    {
        public ApplicationUserStore(ApplicationDbContext context):base(context)
        {
                
        }
    }
}
