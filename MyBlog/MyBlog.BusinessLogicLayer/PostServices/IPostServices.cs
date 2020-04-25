using MyBlog.BusinessLogicLayer.BaseServices;
using MyBlog.DataAccessLayer.Infrastructure;
using MyBlog.Models;

namespace MyBlog.BusinessLogicLayer.PostServices
{
    public interface IPostServices : IBaseServices<Post>
    {

    }

    public class PostServices : BaseServices<Post>, IPostServices
    {
        public PostServices(IGenericRepository<Post> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
