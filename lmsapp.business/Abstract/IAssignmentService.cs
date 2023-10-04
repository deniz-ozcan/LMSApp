using lmsapp.entity;
namespace lmsapp.business.Abstract
{
    public interface IAssignmentService: IService<Assignment>
    {
        Task<List<Assignment>> GetAssignmentsByUserIdAsync(string userId);

    }
}