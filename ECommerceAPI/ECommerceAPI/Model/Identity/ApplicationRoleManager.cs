using Microsoft.AspNetCore.Identity;

namespace ECommerceAPI.Model.Identity
{
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        public ApplicationRoleManager(IRoleStore<ApplicationRole> store,
                                        IEnumerable<IRoleValidator<ApplicationRole>> roleValidators,
                                        ILookupNormalizer keyNormalizer,
                                        IdentityErrorDescriber errors,
                                        ILogger<RoleManager<ApplicationRole>> logger,
                                        IServiceProvider serviceProvider) : base(
                                            store, roleValidators, keyNormalizer, errors, logger)
        {
        }
    }
}
