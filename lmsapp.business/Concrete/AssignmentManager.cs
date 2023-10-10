using lmsapp.business.Abstract;
using lmsapp.data.Abstract;
using lmsapp.entity;

namespace lmsapp.business.Concrete
{
    public class AssignmentManager : IAssignmentService
    {
        private readonly IUnitOfWork _unitofwork;
        public string ErrorMessage { get; set; }
        public AssignmentManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task CreateAsync(Assignment entity)
        {
            await _unitofwork.Assignments.CreateAsync(entity);
            await _unitofwork.SaveAsync();
        }
        public Task<Assignment> UpdateAsync(Assignment entity)
        {
            if (Validation(entity))
            {
                _unitofwork.Assignments.UpdateAsync(entity);
                _unitofwork.Save();
                return Task.FromResult(entity);
            }
            return null;
        }
        public void Delete(Assignment entity)
        {
            // business logic
            _unitofwork.Assignments.Delete(entity);
            _unitofwork.Save();
        }
        public bool Validation(Assignment entity)
        {
            var isValid = true;
            if (string.IsNullOrEmpty(entity.Title))
            {
                ErrorMessage += "Title is required.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(entity.Description))
            {
                ErrorMessage += "Description is required.";
                isValid = false;
            }
            return isValid;
        }
        public Task<List<Assignment>> GetAllAsync()
        {
            return _unitofwork.Assignments.GetAllAsync();
        }
        public async Task<List<Assignment>> GetAssignmentsByUserIdAsync(string userId)
        {
            return await _unitofwork.Assignments.GetAssignmentsByUserIdAsync(userId);
        }

        public Task<List<Assignment>> GetAssignmentsByCourseIdAsync(int courseId)
        {
            return _unitofwork.Assignments.GetAssignmentsByCourseIdAsync(courseId);
        }
    }
}