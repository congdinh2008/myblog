using MyBlog.BusinessLogicLayer.BaseServices;
using MyBlog.DataAccessLayer.Infrastructure;
using MyBlog.Models;

namespace MyBlog.BusinessLogicLayer.TagServices
{
    public interface ITagServices : IBaseServices<Tag>
    {

    }

    public class TagServices : BaseServices<Tag>, ITagServices
    {
        public TagServices(IGenericRepository<Tag> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
