using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace PDFService.Repository.MySQL.Context
{
    sealed class ContextFactory : IDesignTimeDbContextFactory<DefaultContext>
    {
        readonly string _connstring;

        public ContextFactory(string connstring)
        {
            _connstring = connstring;
        }

        public ContextFactory()
        {
            _connstring = "Server=localhost;Port=4306;Uid=root;Pwd=secret;Database=pdfservice";
        }

        public DefaultContext CreateDbContext(string[] args)
        {
            try
            {
                var builder = new DbContextOptionsBuilder<DefaultContext>();
                builder.UseLazyLoadingProxies();
                builder.UseMySql(_connstring);
                return new DefaultContext(builder.Options);
            }
            catch (Exception ex)
            { throw ex; }
        }

        public DefaultContext CreateDbContext()
        {
            try
            {
                return CreateDbContext(null);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
