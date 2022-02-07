namespace WSA.Microservice.Template.Web.Constants
{
    public static class AuthConstants
    {
        public static class Header
        {
            public const string ApiKey = "ApiKey";
            public const string Authorization = "Authorization";
            public const string Bearer = "Bearer";
        }

        public static class Settings
        {
            public const string ApiKey = "ApiKey";
        }

        public static class Messages
        {
            public const string ApiKeyNotFound = "Required subscription key not passed.";
            public const string AuthorizationNotFound = "Authorization header not found.";
            public const string JwtTokenNotFound = "JWT token not present.";
        }

        public static class ClaimType
        {
            public const string Name = "name";
            public const string EMail = "emails";
            public const string CustomerNumber = "extension_widex_identity_company_localcustomernumber";

        }
    }
}
