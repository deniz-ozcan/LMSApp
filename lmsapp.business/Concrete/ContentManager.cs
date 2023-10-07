using lmsapp.business.Abstract;
using lmsapp.data.Abstract;
using lmsapp.entity;

namespace lmsapp.business.Concrete
{
    public class ContentManager : IContentService
    {
        private readonly IUnitOfWork _unitofwork;
        public string ErrorMessage { get; set; }
        public ContentManager(IUnitOfWork unitofwork)
        {
            _unitofwork = unitofwork;
        }
        public async Task CreateAsync(Content entity)
        {
            await _unitofwork.Contents.CreateAsync(entity);
            await _unitofwork.SaveAsync();
        }
        public Task<Content> UpdateAsync(Content entity)
        {
            if (Validation(entity))
            {
                _unitofwork.Contents.UpdateAsync(entity);
                _unitofwork.Save();
                return Task.FromResult(entity);
            }
            return null;
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
            if (string.IsNullOrEmpty(entity.VideoUrl))
            {
                ErrorMessage += "Video Url is required.";
                isValid = false;
            }
            return isValid;
        }
        public Task<List<Content>> GetAllAsync()
        {
            return _unitofwork.Contents.GetAllAsync();
        }
    }
}