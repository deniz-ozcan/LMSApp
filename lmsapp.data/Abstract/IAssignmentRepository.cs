using lmsapp.entity;
namespace lmsapp.data.Abstract
{
    public interface IAssignmentRepository : IRepository<Assignment>
    {
        Task<List<Assignment>> GetAssignmentsByUserIdAsync(string userId);
        Task<List<Assignment>> GetAssignmentsByCourseIdAsync(int courseId);
    }
}