namespace BookmarkManager.Domain.Entities
{
    public class Collection
    {
        public int CollectionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public bool IsFav { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; } = 1;
        public ICollection<Bookmark>? Bookmarks { get; set; }
    }
} 