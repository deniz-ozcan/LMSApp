namespace lmsapp.data.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IAssignmentRepository Assignments { get; }
        ICourseRepository Courses { get; }
        IContentRepository Contents { get; }
        ISectionRepository Sections { get; }
        IEnrollmentRepository Enrollments { get; }
        void Save();
        Task<int> SaveAsync();
    }
}