using BookmarkManager.Domain.Entities;
using BookmarkManager.Domain.Interfaces;
using BookmarkManager.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookmarkManager.Infrastructure.Database.Repository
{
    public class BookmarkRepository : IBookmarkRepository
    {
        private readonly BMDbContext _context;
        private readonly DbSet<Bookmark> _dbSet;

        public BookmarkRepository(BMDbContext context)
        {
            _context = context;
            _dbSet = context.Bookmarks;
        }

        public async Task<Bookmark?> GetByIdAsync(int id)
        {
            return await _dbSet.Include(x => x.Collection).FirstOrDefaultAsync(x => x.BookmarkId == id);
        }

        public async Task<IEnumerable<Bookmark>> GetAllAsync()
        {
            return await _dbSet.Include(x => x.Collection).ToListAsync();
        }

        public async Task AddAsync(Bookmark entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(Bookmark entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(Bookmark entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<Bookmark> Find(Expression<Func<Bookmark, bool>> predicate)
        {
            return _dbSet.Include(x => x.Collection).Where(predicate);
        }
    }
} 