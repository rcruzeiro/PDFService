using Microsoft.EntityFrameworkCore;
using PDFService.Repository.MySQL.Configurations;

namespace PDFService.Repository.MySQL.Context
{
    sealed class DefaultContext : DbContext
    {
        public DefaultContext(DbContextOptions options)
            : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TemplateTypeConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
