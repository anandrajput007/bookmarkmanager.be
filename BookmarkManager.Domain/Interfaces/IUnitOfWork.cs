using System.Threading;
using System.Threading.Tasks;

namespace BookmarkManager.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        ICollectionRepository Collections { get; }
        IBookmarkRepository Bookmarks { get; }
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
        Task DisposeTransactionAsync();
    }
} 