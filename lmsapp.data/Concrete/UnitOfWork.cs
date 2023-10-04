using lmsapp.data.Abstract;
namespace lmsapp.data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LMSContext _context;
        private CourseRepository _courseRepository;
        private AssignmentRepository _assignmentRepository;
        private EnrollmentRepository _enrollmentRepository;
        public UnitOfWork(LMSContext context) { _context = context; }
        public ICourseRepository Courses => _courseRepository = _courseRepository ?? new CourseRepository(_context);
        public IAssignmentRepository Assignments => _assignmentRepository = _assignmentRepository ?? new AssignmentRepository(_context);
        public IEnrollmentRepository Enrollments => _enrollmentRepository = _enrollmentRepository ?? new EnrollmentRepository(_context);

        public void Dispose() => _context.Dispose();
        public void Save() => _context.SaveChanges();
        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
    }
}