using lmsapp.entity;

namespace lmsapp.business.Abstract
{
    public interface IContentService : IGenericService<Content>, IValidator<Content>
    {
        
    }
}