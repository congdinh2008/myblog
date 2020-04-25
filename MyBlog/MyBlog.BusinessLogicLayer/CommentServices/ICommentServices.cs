using MyBlog.BusinessLogicLayer.BaseServices;
using MyBlog.DataAccessLayer.Infrastructure;
using MyBlog.Models;

namespace MyBlog.BusinessLogicLayer.CommentServices
{
    public interface ICommentServices: IBaseServices<Comment>
    {
        
    }

    public class CommentServices : BaseServices<Comment>, ICommentServices
    {
        public CommentServices(IGenericRepository<Comment> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
