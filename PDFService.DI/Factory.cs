using System;
using Microsoft.Extensions.Configuration;
using PDFService.Services.Entity;

namespace PDFService.DI
{
    public sealed class Factory
    {
        static Factory _instance;

        public static Factory Instance
        { get { return _instance ?? (_instance = new Factory()); } }

        public TemplateEntityService GetTemplate(IConfiguration configuration)
        {
            try
            {
                return ServiceFactory.GetTemplateEntityService(configuration);
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
