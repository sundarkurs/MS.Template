using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace WSA.Microservice.Template.Web.Logger
{
    public class CloudRoleNameTelemetryInitializer : ITelemetryInitializer
    {
        private readonly string roleName;

        public CloudRoleNameTelemetryInitializer(string roleName)
        {
            this.roleName = roleName ?? throw new ArgumentNullException(nameof(roleName));
        }

        public void Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Cloud.RoleName = roleName;
        }
    }
}
