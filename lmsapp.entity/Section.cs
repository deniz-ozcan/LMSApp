

namespace lmsapp.entity
{
    public class Section
    {
        public int SectionId { get; set; }
        public string Title { get; set; }
        public Course Course { get; set; }
        public int CourseId { get; set; }
        public List<Content> Contents { get; set; }
    }
}