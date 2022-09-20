using Microsoft.AspNetCore.Authorization;

namespace LoginUser.WebApi.Authorization
{
    //https://youtu.be/Ei7Uk-UgSAY?t=2341
    public enum ResourceOperation
    {
        Create,
        Read,
        Update,
        Delete
    }
    public class ResourceOperationRequirement : IAuthorizationRequirement
    {
        public ResourceOperationRequirement(ResourceOperation resourceOperation)
        {
            ResourceOperation = resourceOperation;
        }
        public ResourceOperation ResourceOperation { get; }
    }
}
