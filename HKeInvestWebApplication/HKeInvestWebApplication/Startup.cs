using IdentityManager;
using IdentityManager.Configuration;
using IdentityManager.AspNetIdentity;
using Microsoft.Owin;
using Owin;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using HKeInvestWebApplication.Models;
using System.Threading.Tasks;
using System.Linq;

[assembly: OwinStartupAttribute(typeof(HKeInvestWebApplication.Startup))]
namespace HKeInvestWebApplication
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            app.Map("/idm", idm =>
             {
                 var factory = new IdentityManagerServiceFactory();
                 factory.IdentityManagerService = new Registration<IIdentityManagerService, ApplicationIdentityManagerService>();
                 factory.Register(new Registration<ApplicationUserManager>());
                 factory.Register(new Registration<ApplicationUserStore>());
                 factory.Register(new Registration<ApplicationRoleManager>());
                 factory.Register(new Registration<ApplicationRoleStore>());
                 factory.Register(new Registration<ApplicationDbContext>());

                 idm.UseIdentityManager(new IdentityManagerOptions
                 {
                     Factory = factory
                 });
             });
        }
    }

        public class ApplicationUserStore : UserStore<ApplicationUser>
        {
            public ApplicationUserStore(ApplicationDbContext ctx)
                : base(ctx)
            {
            }
        }

        public class ApplicationRoleStore : RoleStore<IdentityRole>
        {
            public ApplicationRoleStore(ApplicationDbContext ctx)
                : base(ctx)
            {
            }
        }

        public class ApplicationRoleManager : RoleManager<IdentityRole>
        {
            public ApplicationRoleManager(ApplicationRoleStore roleStore)
                : base(roleStore)
            {
            }
        }

    public class ApplicationIdentityManagerService : 
        AspNetIdentityManagerService<ApplicationUser, string, IdentityRole, string>
    {
        public ApplicationIdentityManagerService(ApplicationUserManager userMgr, ApplicationRoleManager roleMgr)
            : base(userMgr, roleMgr)
        {
        }
        public override async Task<IdentityManagerResult> AddUserClaimAsync(string subject, string type, string value)
        {
            var result = await base.AddUserClaimAsync(subject, type, value);
            if (!result.IsSuccess)
            {
                return new IdentityManagerResult<CreateResult>(result.Errors.ToArray());
            }
            if (type == RoleClaimType)
            {
                var key = ConvertUserSubjectToKey(subject);
                var status = await this.userManager.AddToRoleAsync(key, value);
                if (!status.Succeeded)
                {
                    return new IdentityManagerResult<CreateResult>(result.Errors.ToArray());
                }
            }

            return IdentityManagerResult.Success;
        }

        public override async Task<IdentityManagerResult> RemoveUserClaimAsync(string subject, string type, string value)
        {
            var result = await base.RemoveUserClaimAsync(subject, type, value);
            if (!result.IsSuccess)
            {
                return new IdentityManagerResult<CreateResult>(result.Errors.ToArray());
            }
            if (type == RoleClaimType)
            {
                var key = ConvertUserSubjectToKey(subject);
                var status = await this.userManager.RemoveFromRoleAsync(key, value);
                if (!status.Succeeded)
                {
                    return new IdentityManagerResult<CreateResult>(result.Errors.ToArray());
                }
            }

            return IdentityManagerResult.Success;
        }
    }
}

