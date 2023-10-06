using Microsoft.EntityFrameworkCore;
using lmsapp.data.Abstract;
using lmsapp.entity;

namespace lmsapp.data.Concrete
{
    public class AssignmentRepository : GenericRepository<Assignment>, IAssignmentRepository
    {
        public AssignmentRepository(LMSContext context) : base(context) { }
        private LMSContext LMSContext
        {
            get { return context as LMSContext; }
        }

        public Task<List<Assignment>> GetAssignmentsByUserIdAsync(string userId)
        {
            throw new NotImplementedException();
        }
    }
}