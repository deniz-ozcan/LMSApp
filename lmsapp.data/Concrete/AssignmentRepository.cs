using Microsoft.EntityFrameworkCore;
using lmsapp.data.Abstract;
using lmsapp.entity;

namespace lmsapp.data.Concrete
{
    public class AssignmentRepository : GenericRepository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(LMSContext context) : base(context) { }
        private LMSContext LMSContext
        {
            get { return context as LMSContext; }
        }

        public Task<List<Assignment>> GetAssignmentsByCourseIdAsync(int courseId)
        {
            return LMSContext.Assignments
                .Include(a => a.Course)
                .AsSplitQuery()
                .Include(a => a.Assignees)
                .Where(a => a.CourseId == courseId)
                .ToListAsync();
        }

        public Task<List<Assignment>> GetAssignmentsByUserIdAsync(string userId)
        {
            return LMSContext.Assignments
                .Include(a => a.Assignees)
                .Where(a => a.Assignees.Any(a => a.UserId == userId))
                .ToListAsync();
        }
    }
}