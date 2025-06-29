namespace BookmarkManager.Application.Dto.Bookmarks
{
    public class BookmarkDto
    {
        public int BookmarkId { get; set; }
        public int CollectionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public bool IsFav { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public string CollectionName { get; set; } = string.Empty;
    }
} 