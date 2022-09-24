using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LoginUser.IntegrationTests.Fakes
{
    public class FakeUserFilter : IAsyncActionFilter
    {
        //https://youtu.be/6keSabBQRdE?t=4668
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var claimsPrincipal = new ClaimsPrincipal();

            claimsPrincipal.AddIdentity(new ClaimsIdentity(
                new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, "1"),
                    new Claim(ClaimTypes.Role, "Admin"),
                }));

            context.HttpContext.User = claimsPrincipal;//TODO tutaj przypisujemy claimy

            await next();
        }
    }
}
