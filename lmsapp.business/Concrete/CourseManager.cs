using lmsapp.business.Abstract;
using lmsapp.data.Abstract;
using lmsapp.entity;

namespace lmsapp.business.Concrete
{
    public class CourseManager : ICourseService
    {
        private readonly IUnitOfWork _unitofwork;
        public string ErrorMessage { get; set; }
        public CourseManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task CreateAsync(Course entity)
        {
            await _unitofwork.Courses.CreateAsync(entity);
            await _unitofwork.SaveAsync();
        }
        public Task<Course> UpdateAsync(Course entity)
        {
            if (Validation(entity))
            {
                _unitofwork.Courses.UpdateAsync(entity);
                _unitofwork.SaveAsync();
                return Task.FromResult(entity);
            }
            return null;
        }
        public void Delete(Course entity)
        {
            _unitofwork.Courses.Delete(entity);
            _unitofwork.Save();
        }
        public Task<Course> GetCourseByIdAsync(int id)
        {
            return _unitofwork.Courses.GetCourseByIdAsync(id);
        }
        public Task<List<Course>> GetCoursesAsync(string q, int page, int pageSize)
        {
            return _unitofwork.Courses.GetCoursesAsync(q, page, pageSize);
        }
        public Task<int> GetCoursesCountAsync(string q)
        {
            return _unitofwork.Courses.GetCoursesCountAsync(q);
        }
        public Task<List<Course>> GetInstructorCoursesByUserIdAsync(string userId)
        {
            return _unitofwork.Courses.GetInstructorCoursesByUserIdAsync(userId);
        }
        public Task<List<Course>> GetStudentCoursesByUserIdAsync(string userId)
        {
            return _unitofwork.Courses.GetStudentCoursesByUserIdAsync(userId);
        }
        public bool Validation(Course entity)
        {
            var isValid = true;
            if (string.IsNullOrEmpty(entity.Title))
            {
                ErrorMessage += "Title is required.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(entity.Description))
            {
                ErrorMessage += "Description is required.";
                isValid = false;
            }
            return isValid;
        }
        public Task<List<Course>> GetAllAsync()
        {
            return _unitofwork.Courses.GetAllAsync();
        }

        public Task<Course> GetStudentCourseContentAsync(string userId, int courseId)
        {
            return _unitofwork.Courses.GetStudentCourseContentAsync(userId, courseId);
        }

        public Task<Course> GetInstructorCourseContentAsync(int courseId)
        {
            return _unitofwork.Courses.GetInstructorCourseContentAsync(courseId);
        }

        public Task<Course> GetCourseContentByIdAsync(int courseId)
        {
            return _unitofwork.Courses.GetCourseContentByIdAsync(courseId);
        }

        public Task<List<Course>> GetFilteredCoursesAsync(string q, float rate, string sortBy, string level, int page, int pageSize)
        {
            return _unitofwork.Courses.GetFilteredCoursesAsync(q, rate, sortBy, level, page, pageSize);
        }

        public Task<int> GetFilteredCoursesCountAsync(string q, float rate, string sortBy, string level)
        {
            return _unitofwork.Courses.GetFilteredCoursesCountAsync(q, rate, sortBy, level);
        }
    }
}