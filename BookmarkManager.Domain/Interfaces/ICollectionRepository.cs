using BookmarkManager.Domain.Entities;

namespace BookmarkManager.Domain.Interfaces
{
    public interface ICollectionRepository : IRepository<Collection>, IAggregateRoot
    {
        // Add custom methods if needed
    }
} 