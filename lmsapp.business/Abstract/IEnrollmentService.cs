using lmsapp.entity;

namespace lmsapp.business.Abstract
{
    public interface IEnrollmentService : IService<Enrollment>
    {
        void Enroll(int CourseId, string userId);
    }
}