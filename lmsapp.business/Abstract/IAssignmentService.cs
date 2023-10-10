using lmsapp.entity;
namespace lmsapp.business.Abstract
{
    public interface IAssignmentService: IGenericService<Assignment>
    {
        Task<List<Assignment>> GetAssignmentsByUserIdAsync(string userId);
        Task<List<Assignment>> GetAssignmentsByCourseIdAsync(int courseId);
    }
}