

using lmsapp.business.Abstract;
using lmsapp.data.Abstract;
using lmsapp.entity;

namespace lmsapp.business.Concrete
{
    public class EnrollmentManager : IEnrollmentService
    {
        private readonly IUnitOfWork _unitofwork;
        public string ErrorMessage { get; set; }
        public EnrollmentManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task CreateAsync(Enrollment entity)
        {
            await _unitofwork.Enrollments.CreateAsync(entity);
            await _unitofwork.SaveAsync();
        }
        public Task<Enrollment> UpdateAsync(Enrollment entity)
        {
            _unitofwork.Enrollments.UpdateAsync(entity);
            _unitofwork.Save();
            return Task.FromResult(entity);
        }
        public void Delete(Enrollment entity)
        {
            _unitofwork.Enrollments.Delete(entity);
            _unitofwork.Save();
        }
        public bool isEnrolled(int courseId, string userId)
        {
            return _unitofwork.Enrollments.isEnrolled(courseId, userId);
        }
        public Task<List<Enrollment>> GetAllAsync()
        {
            return _unitofwork.Enrollments.GetAllAsync();
        }
    }
}