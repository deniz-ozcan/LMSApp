
using lmsapp.entity;

namespace lmsapp.data.Abstract
{
    public interface IEnrollmentRepository : IRepository<Enrollment>
    {
        void Enroll(int CourseId, string userId);
    }
}