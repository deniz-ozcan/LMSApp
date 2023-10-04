using lmsapp.business.Abstract;
using lmsapp.data.Abstract;
using lmsapp.entity;

namespace lmsapp.business.Concrete
{
    public class EnrollmentManager : IEnrollmentService
    {
        private readonly IUnitOfWork _unitofwork;
        public EnrollmentManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public void EnrollAsync(Enrollment entity, string userId)
        {
            _unitofwork.Enrollments.EnrollAsync(entity, userId);
        }

    }
}