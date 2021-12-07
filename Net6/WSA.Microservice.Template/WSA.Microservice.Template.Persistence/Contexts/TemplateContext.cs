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

        public virtual DbSet<Domain.Models.Template> Templates { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tst-identity-eu-n-wdx-sql.database.windows.net;Database=tst-ms-template-sdb;Trusted_Connection=False;Encrypt=True;uid=widex_db@tst-identity-eu-n-wdx-sql;password=W!dex123");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Models.Template>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("Template");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
