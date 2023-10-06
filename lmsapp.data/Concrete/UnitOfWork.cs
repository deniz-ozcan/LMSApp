using lmsapp.data.Abstract;
namespace lmsapp.data.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LMSContext _context;
        private CourseRepository _courseRepository;
        private AssignmentRepository _assignmentRepository;
        private SectionRepository _SectionRepository;
        private ContentRepository _contentRepository;
        public UnitOfWork(LMSContext context) { _context = context; }
        public ICourseRepository Courses => _courseRepository = _courseRepository ?? new CourseRepository(_context);
        public IAssignmentRepository Assignments => _assignmentRepository = _assignmentRepository ?? new AssignmentRepository(_context);
        public ISectionRepository Sections => _SectionRepository = _SectionRepository ?? new SectionRepository(_context);
        public IContentRepository Contents => _contentRepository = _contentRepository ?? new ContentRepository(_context);
        public void Dispose() => _context.Dispose();
        public void Save() => _context.SaveChanges();
        public async Task<int> SaveAsync() => await _context.SaveChangesAsync();
    }
}