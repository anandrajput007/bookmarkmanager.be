using MediatR;
using BookmarkManager.Application.Dto.Bookmarks;

namespace BookmarkManager.Application.Commands.Bookmarks
{
    public class CreateBookmarkCommand : IRequest<BookmarkDto>
    {
        public int CollectionId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public bool IsFav { get; set; }
    }
} 