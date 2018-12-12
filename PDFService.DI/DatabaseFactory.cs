using System;
using Microsoft.Extensions.Configuration;
using PDFService.Repository;
using PDFService.Repository.MySQL;

namespace PDFService.DI
{
    static class DatabaseFactory
    {
        static readonly string _conn =
            Environment.GetEnvironmentVariable("DB_CONNECTION");

        public static ITemplateRepository GetTemplateRepository(IConfiguration configuration)
        {
            try
            {
                return new TemplateRepository(configuration.GetConnectionString(_conn));
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
