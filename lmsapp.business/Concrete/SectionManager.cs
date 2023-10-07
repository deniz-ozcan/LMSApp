using lmsapp.business.Abstract;
using lmsapp.data.Abstract;
using lmsapp.entity;

namespace lmsapp.business.Concrete
{
    public class SectionManager : ISectionService
    {
        private readonly IUnitOfWork _unitofwork;
        public string ErrorMessage { get; set; }
        public SectionManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task CreateAsync(Section entity)
        {
            await _unitofwork.Sections.CreateAsync(entity);
            await _unitofwork.SaveAsync();
        }
        public Task<Section> UpdateAsync(Section entity)
        {
            _unitofwork.Sections.UpdateAsync(entity);
            _unitofwork.Save();
            return Task.FromResult(entity);
        }
        public void Delete(Section entity)
        {
            _unitofwork.Sections.Delete(entity);
            _unitofwork.Save();
        }
        public bool Validation(Section entity)
        {
            var isValid = true;
            if (string.IsNullOrEmpty(entity.Title))
            {
                ErrorMessage += "Title is required.";
                isValid = false;
            }
            return isValid;
        }
        public Task<List<Section>> GetAllAsync()
        {
            return _unitofwork.Sections.GetAllAsync();
        }
    }
}