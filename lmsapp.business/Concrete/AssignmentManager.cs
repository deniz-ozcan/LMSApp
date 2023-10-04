

using lmsapp.business.Abstract;
using lmsapp.data.Abstract;
using lmsapp.entity;

namespace lmsapp.business.Concrete
{
    public class AssignmentManager : IAssignmentService
    {
        private readonly IUnitOfWork _unitofwork;
        public AssignmentManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public string ErrorMessage { get; set; }

        public async Task<Assignment> CreateAsync(Assignment entity)
        {
            await _unitofwork.Assignments.CreateAsync(entity);
            await _unitofwork.SaveAsync();
            return entity;
        }

        public bool Update(Assignment entity)
        {
            if (Validation(entity))
            {
                _unitofwork.Assignments.Update(entity);
                _unitofwork.Save();
                return true;
            }
            return false;
        }
        public void Delete(Assignment entity)
        {
            // business logic
            _unitofwork.Assignments.Delete(entity);
            _unitofwork.Save();
        }
        public async Task<List<Assignment>> GetAssignmentsByUserIdAsync(string userId)
        {
            return await _unitofwork.Assignments.GetAssignmentsByUserIdAsync(userId);
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
    }
}