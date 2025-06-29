using BookmarkManager.Domain.Entities;

namespace BookmarkManager.Domain.Interfaces
{
    public interface IBookmarkRepository : IRepository<Bookmark>, IAggregateRoot
    {
        // Add custom methods if needed
    }
} 