using lmsapp.business.Abstract;
using lmsapp.data.Abstract;
using lmsapp.entity;

namespace lmsapp.business.Concrete
{
    public class ContentManager : IContentService
    {
        private readonly IUnitOfWork _unitofwork;
        public ContentManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }

        public string ErrorMessage { get; set; }
        public async Task<Content> CreateAsync(Content entity)
        {
            await _unitofwork.Contents.CreateAsync(entity);
            await _unitofwork.SaveAsync();
            return entity;
        }

        public bool Update(Content entity)
        {            
            if (Validation(entity))
            {
                _unitofwork.Contents.Update(entity);
                _unitofwork.Save();
                return true;
            }
            return false;
        }
        public void Delete(Content entity)
        {
            _unitofwork.Contents.Delete(entity);
            _unitofwork.Save();
        }
        public bool Validation(Content entity)
        {
            var isValid = true;
            if (string.IsNullOrEmpty(entity.Title))
            {
                ErrorMessage += "Title is required.";
                isValid = false;
            }
            if (string.IsNullOrEmpty(entity.Description))
            {
                ErrorMessage += "Description is required.";
                isValid = false;
            }
            return isValid;
        }
    }
}