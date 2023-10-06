using Microsoft.EntityFrameworkCore;
using lmsapp.data.Abstract;
using lmsapp.entity;
using Microsoft.AspNetCore.Identity;

namespace lmsapp.data.Concrete
{
    public class ContentRepository : GenericRepository<Content>, IContentRepository
    {
        public ContentRepository(LMSContext context) : base(context) { }
        private LMSContext LMSContext
        {
            get { return context as LMSContext; }
        }
    }
}