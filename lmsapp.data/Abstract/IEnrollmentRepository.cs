using lmsapp.entity;
namespace lmsapp.data.Abstract
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        void EnrollAsync(Enrollment entity, string userId);
    }
}