using System;
using PDFService.DTO;
using PDFService.Entities;

namespace PDFService.Adapter
{
    public static class TemplateAdapter
    {
        public static Template Adapt(this TemplateDTO template)
        {
            if (template == null)
                throw new ArgumentNullException(nameof(template));

            try
            {
                return new Template
                {
                    ID = template.ID,
                    ClientID = template.ClientID,
                    Title = template.Title,
                    Description = template.Description,
                    Page = template.Page,
                    Content = template.Content
                };
            }
            catch (Exception ex)
            { throw ex; }
        }

        public static TemplateDTO Adapt(this Template template)
        {
            if (template == null)
                throw new ArgumentNullException(nameof(template));

            try
            {
                return new TemplateDTO
                {
                    ID = template.ID,
                    ClientID = template.ClientID,
                    Title = template.Title,
                    Description = template.Description,
                    Page = template.Page,
                    Content = template.Content
                };
            }
            catch (Exception ex)
            { throw ex; }
        }
    }
}
