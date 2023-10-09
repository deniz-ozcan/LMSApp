using lmsapp.business.Abstract;
using lmsapp.data.Abstract;
using lmsapp.entity;

namespace lmsapp.business.Concrete
{
    public class AssigneeManager : IAssigneeService
    {

        private readonly IUnitOfWork _unitofwork;
        public string ErrorMessage { get; set; }
        public AssigneeManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task CreateAsync(Assignee entity)
        {
            await _unitofwork.Assignees.CreateAsync(entity);
            await _unitofwork.SaveAsync();
        }
        public Task<Assignee> UpdateAsync(Assignee entity)
        {
            _unitofwork.Assignees.UpdateAsync(entity);
            _unitofwork.Save();
            return Task.FromResult(entity);
        }
        public void Delete(Assignee entity)
        {
            _unitofwork.Assignees.Delete(entity);
            _unitofwork.Save();
        }
        public Task<List<Assignee>> GetAllAsync()
        {
            return _unitofwork.Assignees.GetAllAsync();
        }

    }
}