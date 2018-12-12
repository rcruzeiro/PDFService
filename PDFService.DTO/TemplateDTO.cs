namespace PDFService.DTO
{
    public sealed class TemplateDTO
    {
        public int ID { get; set; }
        public string ClientID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Page { get; set; }
        public string Content { get; set; }
    }
}
