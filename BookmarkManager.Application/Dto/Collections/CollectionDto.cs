namespace BookmarkManager.Application.Dto.Collections
{
    public class CollectionDto
    {
        public int CollectionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public bool IsFav { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CreatedBy { get; set; }
        public int BookmarkCount { get; set; }
    }
} 