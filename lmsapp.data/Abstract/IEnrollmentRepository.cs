
using lmsapp.entity;

namespace lmsapp.data.Abstract
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        bool isEnrolled(int courseId, string userId);
    }
}