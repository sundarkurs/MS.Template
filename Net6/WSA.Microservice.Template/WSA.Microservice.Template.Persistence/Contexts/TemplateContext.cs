using Microsoft.EntityFrameworkCore;

namespace WSA.Microservice.Template.Persistence.Contexts
{
    public partial class TemplateContext : DbContext
    {
        public TemplateContext()
        {
        }

        public TemplateContext(DbContextOptions<TemplateContext> options)
            : base(options)
        {
        }
    }
}
