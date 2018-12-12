using System.Collections.Generic;
using Core.Framework.API.Messages;
using PDFService.DTO;

namespace PDFService.API.Messages.Templates
{
    public class GetTemplatesResponse : BaseResponse<List<TemplateDTO>>
    {
        public GetTemplatesResponse()
        {
            Data = new List<TemplateDTO>();
        }
    }
}
