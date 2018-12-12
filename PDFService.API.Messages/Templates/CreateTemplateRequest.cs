using Core.Framework.API.Messages;

namespace PDFService.API.Messages.Templates
{
    public class CreateTemplateRequest : BaseRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Page { get; set; }
        public string Content { get; set; }
    }
}
