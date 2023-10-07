using lmsapp.business.Abstract;
using lmsapp.data.Abstract;
using lmsapp.entity;

namespace lmsapp.business.Concrete
{
    public class SectionManager : ISectionService
    {
        private readonly IUnitOfWork _unitofwork;
        public SectionManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public string ErrorMessage { get; set; }

        public Task<Section> CreateAsync(Section entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Section entity)
        {
            throw new NotImplementedException();
        }

        public Task<Section> UpdateAsync(Section entity)
        {
            throw new NotImplementedException();
        }

        public bool Validation(Section entity)
        {
            throw new NotImplementedException();
        }
    }
}