namespace lmsapp.data.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IEnrollmentRepository Enrollments { get; }
        IAssignmentRepository Assignments { get; }
        ICourseRepository Courses { get; }
        void Save();
        Task<int> SaveAsync();
    }
}