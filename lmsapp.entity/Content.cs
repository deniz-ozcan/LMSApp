namespace lmsapp.entity
{
    public class Content
    {
        public int ContentId { get; set; }
        public Section Section { get; set; }
        public int SectionId { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
    }
}