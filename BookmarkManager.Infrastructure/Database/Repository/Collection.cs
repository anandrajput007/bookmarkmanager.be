using BookmarkManager.Domain.Entities;
using BookmarkManager.Domain.Interfaces;
using BookmarkManager.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BookmarkManager.Infrastructure.Database.Repository
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly BMDbContext _context;
        private readonly DbSet<Collection> _dbSet;

        public CollectionRepository(BMDbContext context)
        {
            _context = context;
            _dbSet = context.Collections;
        }

        public async Task<Collection?> GetByIdAsync(int id)
        {
            return await _dbSet.Include(x => x.Bookmarks).FirstOrDefaultAsync(x => x.CollectionId == id);
        }

        public async Task<IEnumerable<Collection>> GetAllAsync()
        {
            return await _dbSet.Include(x => x.Bookmarks).ToListAsync();
        }

        public async Task AddAsync(Collection entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(Collection entity)
        {
            _dbSet.Update(entity);
        }

        public void Remove(Collection entity)
        {
            _dbSet.Remove(entity);
        }

        public IQueryable<Collection> Find(Expression<Func<Collection, bool>> predicate)
        {
            return _dbSet.Include(x => x.Bookmarks).Where(predicate);
        }
    }
} 