using lmsapp.entity;
namespace lmsapp.data.Abstract
{
    public interface ICourseRepository : IRepository<Course>
    {
        Task<List<Course>> GetStudentCoursesByUserIdAsync(string userId);
        Task<Course> GetStudentCourseContentAsync(string userId, int courseId);
        Task<List<Course>> GetInstructorCoursesByUserIdAsync(string userId);
        Task<Course> GetCourseByIdAsync(int id);
        Task<List<Course>> GetCoursesAsync(string q, int page, int pageSize);
        Task<int> GetCoursesCountAsync(string q);
        Task<List<Course>> GetAllCoursesAsync();
    }
}