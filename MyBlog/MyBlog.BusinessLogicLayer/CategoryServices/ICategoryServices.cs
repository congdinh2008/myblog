using MyBlog.BusinessLogicLayer.BaseServices;
using MyBlog.DataAccessLayer.Infrastructure;
using MyBlog.Models;

namespace MyBlog.BusinessLogicLayer.CategoryServices
{
    public interface ICategoryServices : IBaseServices<Category>
    {

    }

    public class CategoryServices : BaseServices<Category>, ICategoryServices
    {
        public CategoryServices(IGenericRepository<Category> repository, IUnitOfWork unitOfWork) : base(repository, unitOfWork)
        {
        }
    }
}
