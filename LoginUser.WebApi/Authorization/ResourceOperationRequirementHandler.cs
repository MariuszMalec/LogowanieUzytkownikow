using LoginUser.WebApi.Entities;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginUser.WebApi.Authorization
{
    public class ResourceOperationRequirementHandler : AuthorizationHandler<ResourceOperationRequirement, Client>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, ResourceOperationRequirement requirement,
            Client client)
        {
            if (requirement.ResourceOperation == ResourceOperation.Create ||
                requirement.ResourceOperation == ResourceOperation.Delete)
            {
                context.Succeed(requirement);
            }

            if (!context.User.Identity.IsAuthenticated)//TODO sprawdza czy zalogowany
            {
                context.Fail();
                return Task.CompletedTask;
            }

            var userId = context.User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (client.CreatedById == int.Parse(userId))//https://youtu.be/Ei7Uk-UgSAY?t=2571
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
                return Task.CompletedTask;
            }

            return Task.CompletedTask;
        }
    }
}
