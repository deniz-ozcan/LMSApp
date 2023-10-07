using lmsapp.entity;

namespace lmsapp.business.Abstract
{
    public interface IEnrollmentService : IService<Enrollment>
    {
        bool isEnrolled(int courseId, string userId);
    }
}