using MediatR;
using BookmarkManager.Application.Dto.Collections;

namespace BookmarkManager.Application.Commands.Collections
{
    public class CreateCollectionCommand : IRequest<CollectionDto>
    {
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
        public bool IsFav { get; set; }
    }
} 