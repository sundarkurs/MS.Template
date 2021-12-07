using Microsoft.AspNetCore.Authorization;

namespace WSA.Microservice.Template.Web.Auth
{
    public class ViewTokenRequirement : IAuthorizationRequirement
    {
        public ViewTokenRequirement(bool allowViewToken) =>
            AllowViewToken = allowViewToken;

        public bool AllowViewToken { get; }
    }
}
