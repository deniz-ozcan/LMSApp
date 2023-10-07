using lmsapp.entity;

namespace lmsapp.webui.Models
{
    public class PageInfo
    {
        public int TotalItems { get; set; }
        public int ItemsPerPage { get; set; }
        public int CurrentPage { get; set; }
        public string CurrentCategory { get; set; }
        public int TotalPages() => (int)Math.Ceiling((decimal)TotalItems / ItemsPerPage);
        public int HasPrevious() => CurrentPage > 1 ? 1 : 0;
        public int HasNext() => CurrentPage < TotalPages() ? 1 : 0;
    }

    public class CourseViewModel
    {
        public PageInfo PageInfo { get; set; }
        public List<Course> Courses { get; set; }
    }
}