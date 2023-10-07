using lmsapp.data.Abstract;
using lmsapp.entity;

namespace lmsapp.data.Concrete
{
    public class EnrollmentRepository : GenericRepository<Enrollment>, IEnrollmentRepository
    {
        public EnrollmentRepository(LMSContext context) : base(context) { }
        private LMSContext LMSContext
        {
            get { return context as LMSContext; }
        }

        public bool isEnrolled(int courseId, string userId)
        {
            var enrollment = LMSContext.Enrollments.FirstOrDefault(e => e.CourseId == courseId && e.UserId == userId);
            if (enrollment != null)
            {
                return true;
            }
            return false;
        }
    }
}