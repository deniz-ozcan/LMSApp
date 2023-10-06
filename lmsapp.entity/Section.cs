

namespace lmsapp.entity
{
    public class Section
    {
        public int SectionID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Course Course { get; set; }
        public int CourseID { get; set; }
        public string VideoUrl { get; set; }
        public bool IsCompleted { get; set; }
        public List<Content> Contents { get; set; }
    }
}