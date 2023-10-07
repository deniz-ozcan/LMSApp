using Microsoft.EntityFrameworkCore;
using lmsapp.data.Abstract;
using lmsapp.entity;
using Microsoft.AspNetCore.Identity;


namespace lmsapp.data.Concrete
{
    public class CourseRepository : GenericRepository<Course>, ICourseRepository
    {
        public CourseRepository(LMSContext context) : base(context) { }
        private LMSContext LMSContext
        {
            get { return context as LMSContext; }
        }


        public Task<List<Course>> GetAllCoursesAsync()
        {
            return LMSContext.Courses
                .Include(c => c.Instructor)
                .ToListAsync();
        }

        public Task<Course> GetCourseByIdAsync(int id)
        {
            return LMSContext.Courses
                .Include(c => c.Instructor)
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public Task<List<Course>> GetCoursesAsync(string q, int page, int pageSize)
        {
            return LMSContext.Courses
                .Include(c => c.Instructor)
                .OrderBy(c => c.Id)
                .Where(c => string.IsNullOrEmpty(q) || c.Title.Contains(q) || c.Description.Contains(q))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public Task<int> GetCoursesCountAsync(string q)
        {
            return LMSContext.Courses
                .Where(c => string.IsNullOrEmpty(q) || c.Title.Contains(q) || c.Description.Contains(q))
                .CountAsync();
        }

        public Task<List<Course>> GetInstructorCoursesByUserIdAsync(string userId)
        {
            return LMSContext.Courses
                .Include(c => c.Instructor)
                .Where(c => c.Instructor.Id == userId)
                .ToListAsync();
        }

        public Task<List<Course>> GetStudentCoursesByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}