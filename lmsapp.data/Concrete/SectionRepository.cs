using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using lmsapp.data.Abstract;
using lmsapp.entity;

namespace lmsapp.data.Concrete
{
    public class SectionRepository : GenericRepository<Section>, ISectionRepository
    {
        public SectionRepository(LMSContext context) : base(context) { }
        private LMSContext LMSContext
        {
            get { return context as LMSContext; }
        }
    }
}