using lmsapp.entity;

namespace lmsapp.business.Abstract
{
    public interface ICourseService : IService<Course>
    {
        Task<List<Course>> GetStudentCoursesByUserIdAsync(string userId);
        Task<List<Course>> GetInstructorCoursesByUserIdAsync(string userId);
        Task<Course> GetCourseByIdAsync(int id);
        Task<List<Course>> GetCoursesAsync(string q, int page, int pageSize);
        Task<int> GetCoursesCountAsync(string q);
        void Enroll(int CourseId, User user);
        Task<List<Course>> GetAllCoursesAsync();
    }
}