namespace PDFService.Entities
{
    public class Template : BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Page { get; set; }
        public string Content { get; set; }
    }
}
