namespace lmsapp.entity
{
    public class Content
    {
        public int ContentID { get; set; }
        public Section Section { get; set; }
        public int SectionId { get; set; }
        public string Title { get; set; }
        public string VideoUrl { get; set; }
        public bool IsCompleted { get; set; }
    }
}