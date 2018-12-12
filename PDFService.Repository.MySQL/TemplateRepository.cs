using PDFService.Entities;
using PDFService.Repository.MySQL.Context;

namespace PDFService.Repository.MySQL
{
    public sealed class TemplateRepository : BaseRepository<Template>, ITemplateRepository
    {
        public TemplateRepository(string connstring)
            : base(new ContextFactory(connstring).CreateDbContext())
        { }
    }
}
