

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
        public string ErrorMessage { get; set; }

        public async Task<Enrollment> CreateAsync(Enrollment entity)
        {
            await _unitofwork.Enrollments.CreateAsync(entity);
            await _unitofwork.SaveAsync();
            return entity;
        }
        public bool Update(Enrollment entity)
        {
            _unitofwork.Enrollments.Update(entity);
            _unitofwork.Save();
            return true;
        }
        public void Delete(Enrollment entity)
        {
            _unitofwork.Enrollments.Delete(entity);
            _unitofwork.Save();
        }
    }
}