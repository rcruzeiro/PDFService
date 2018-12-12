using System;
using Microsoft.Extensions.Configuration;
using PDFService.Services.Entity;

namespace PDFService.DI
{
    static class ServiceFactory
    {
        public static TemplateEntityService GetTemplateEntityService(IConfiguration configuration)
        {
            try
            {
                return new TemplateEntityService(DatabaseFactory.GetTemplateRepository(configuration));
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
