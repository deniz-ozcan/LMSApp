using lmsapp.business.Abstract;
using lmsapp.data.Abstract;
using lmsapp.entity;

namespace lmsapp.business.Concrete
{
    public class CourseManager : ICourseService
    {
        private readonly IUnitOfWork _unitofwork;
        public CourseManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public string ErrorMessage { get; set; }
        public async Task<Course> CreateAsync(Course entity)
        {
            await _unitofwork.Courses.CreateAsync(entity);
            await _unitofwork.SaveAsync();
            return entity;
        }

        public bool Update(Course entity)
        {            
            if (Validation(entity))
            {
                _unitofwork.Courses.Update(entity);
                _unitofwork.Save();
                return true;
            }
            return false;
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
    }
}