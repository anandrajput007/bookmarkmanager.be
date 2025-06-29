using BookmarkManager.Domain.Interfaces;
using BookmarkManager.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.DependencyInjection;
using System.Threading;
using System.Threading.Tasks;

namespace BookmarkManager.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BMDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private IDbContextTransaction? _currentTransaction;

        public ICollectionRepository Collections => _serviceProvider.GetRequiredService<ICollectionRepository>();
        public IBookmarkRepository Bookmarks => _serviceProvider.GetRequiredService<IBookmarkRepository>();

        public UnitOfWork(BMDbContext context, IServiceProvider serviceProvider)
        {
            _context = context;
            _serviceProvider = serviceProvider;
        }

        public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
        {
            return await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task BeginTransactionAsync()
        {
            _currentTransaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.CommitAsync();
            }
        }

        public async Task RollbackTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.RollbackAsync();
            }
        }

        public async Task DisposeTransactionAsync()
        {
            if (_currentTransaction != null)
            {
                await _currentTransaction.DisposeAsync();
                _currentTransaction = null;
            }
        }
    }
} 