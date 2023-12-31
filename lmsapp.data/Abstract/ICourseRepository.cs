using lmsapp.entity;
namespace lmsapp.data.Abstract
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<List<Course>> GetStudentCoursesByUserIdAsync(string userId);
        Task<Course> GetCourseContentByIdAsync(int courseId);
        Task<Course> GetStudentCourseContentAsync(string userId, int courseId);
        Task<Course> GetInstructorCourseContentAsync(int courseId);
        Task<List<Course>> GetInstructorCoursesByUserIdAsync(string userId);
        Task<Course> GetCourseByIdAsync(int id);
        Task<List<Course>> GetCoursesAsync(int page, int pageSize);
        Task<int> GetCoursesCountAsync();
        Task<List<Course>> GetAllCoursesAsync();
        Task<List<Course>> GetFilteredCoursesAsync(string q, float rate, string sortBy, string level, int page, int pageSize);
        Task<int> GetFilteredCoursesCountAsync(string q, float rate, string sortBy, string level);
    }
}