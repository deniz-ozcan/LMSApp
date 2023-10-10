
using lmsapp.data.Abstract;
using lmsapp.entity;
using Microsoft.EntityFrameworkCore;

namespace lmsapp.data.Concrete
{
    public class AssigneeRepository : GenericRepository<Assignee>, IAssigneeRepository
    {
        public AssigneeRepository(LMSContext context) : base(context) { }
        private LMSContext LMSContext
        {
            get { return context as LMSContext; }
        }
    }
}