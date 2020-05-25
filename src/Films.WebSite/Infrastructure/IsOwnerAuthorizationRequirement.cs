using System.Security.Claims;
using System.Threading.Tasks;

using Films.WebSite.Data;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace Films.WebSite.Infrastructure
{
    public class IsOwnerAuthorizationRequirement : IAuthorizationRequirement
    { }

    public class IsOwnerAuthorizationHandler : AuthorizationHandler<IsOwnerAuthorizationRequirement, int>
    {
        private readonly DataContext dataContext;

        public IsOwnerAuthorizationHandler(DataContext context)
        {
            this.dataContext = context;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, IsOwnerAuthorizationRequirement requirement, int resource)
        {
            // resource is movie id user wants to edit.
            var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var isOwner = await dataContext.Films.AnyAsync(f => f.Id == resource && f.CreatorId == userId);

            if(isOwner)
            {
                context.Succeed(requirement);
            }
        }
    }
}
