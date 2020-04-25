using System.Threading.Tasks;

namespace MyBlog.DataAccessLayer.Infrastructure
{
    public interface IUnitOfWork
    {
        int Commit();

        Task<int> CommitAsync();
    }
}