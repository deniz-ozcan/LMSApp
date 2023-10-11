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
            return LMSContext.Courses
                    .Where(c => c.InstructorId == userId)
                    .ToListAsync();
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
        public Task<Course> GetInstructorCourseContentAsync(int courseId)
        {
            return LMSContext.Courses
                    .Include(a => a.Assignments)
                    .AsSplitQuery()
                    .Include(s => s.Sections)
                    .ThenInclude(c => c.Contents)
                    .SingleOrDefaultAsync(c => c.Id == courseId);
        }

        public Task<Course> GetCourseContentByIdAsync(int courseId)
        {
            return LMSContext.Courses
                .Include(a => a.Assignments)
                .AsSplitQuery()
                .Include(s => s.Sections)
                .ThenInclude(c => c.Contents)
                .SingleOrDefaultAsync(c => c.Id == courseId);
        }

        public Task<List<Course>> GetFilteredCoursesAsync(string q, float rate, string sortBy, string level, int page, int pageSize)
        {
            var courses = LMSContext.Courses
                        .OrderBy(c => c.Id)
                        .Where(c => string.IsNullOrEmpty(q) || c.Title.Contains(q) || c.Description.Contains(q))
                        .Where(c => rate == 0 || c.Rate >= rate)
                        .Where(c => string.IsNullOrEmpty(level) || c.Level == level)
                        .Skip((page - 1) * pageSize)
                        .Take(pageSize).AsQueryable();
            if(sortBy == "rate")
            {
                courses = courses.OrderByDescending(c => c.Rate);
            }
            else if(sortBy == "-rate")
            {
                courses = courses.OrderBy(c => c.Rate);
            }
            return courses.ToListAsync();
        }

        public Task<int> GetFilteredCoursesCountAsync(string q, float rate, string sortBy, string level)
        {
            return LMSContext.Courses
                        .Where(c => string.IsNullOrEmpty(q) || c.Title.ToUpper().Contains(q.ToUpper()) || c.Description.ToUpper().Contains(q.ToUpper()))
                        .Where(c => rate == 0 || c.Rate >= rate)
                        .Where(c => string.IsNullOrEmpty(level) || c.Level == level)
                        .CountAsync();
        }
    }
}