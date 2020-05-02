using MyBlog.DataAccessLayer.Data;
using System.Threading.Tasks;

namespace MyBlog.DataAccessLayer.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly MyBlogDbContext _context;

        public UnitOfWork(MyBlogDbContext context)
        {
            _context = context;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}