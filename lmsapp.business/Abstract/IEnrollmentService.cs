using lmsapp.entity;
namespace lmsapp.business.Abstract
{
    public interface IEnrollmentService
    {
        void EnrollAsync(Enrollment entity, string userId);
    }
}