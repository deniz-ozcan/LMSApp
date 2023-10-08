using Microsoft.EntityFrameworkCore;
using lmsapp.data.Abstract;
using lmsapp.entity;


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
                .ToListAsync();
        }

        public Task<Course> GetCourseByIdAsync(int id)
        {
            return LMSContext.Courses
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public Task<List<Course>> GetCoursesAsync(string q, int page, int pageSize)
        {
            return LMSContext.Courses
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
            return LMSContext.Courses.Where(c => c.InstructorId == userId).ToListAsync();
        }

        public Task<List<Course>> GetStudentCoursesByUserIdAsync(string userId)
        {
            return LMSContext.Courses
                .Include(c => c.Enrollments)
                .Where(c => c.Enrollments.Any(e => e.UserId == userId)).ToListAsync();
        }
        public Task<Course> GetStudentCourseContentAsync(string userId, int courseId)
        {
            return LMSContext.Courses
                .Include(e => e.Enrollments)
                .Include(a => a.Assignments)
                .AsSplitQuery()
                .Include(s => s.Sections)
                .ThenInclude(c => c.Contents)
                .SingleOrDefaultAsync(c => c.Id == courseId && c.Enrollments.Any(e => e.UserId == userId));
        }
    }
}