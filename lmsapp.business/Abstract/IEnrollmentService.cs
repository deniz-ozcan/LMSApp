using lmsapp.entity;

namespace lmsapp.business.Abstract
{
    public interface IEnrollmentService : IGenericService<Enrollment>
    {
        bool isEnrolled(int courseId, string userId);
    }
}