using BookmarkManager.Application.Dto.Bookmarks;
using BookmarkManager.Domain.Interfaces;
using MediatR;

namespace BookmarkManager.Application.Queries.Bookmarks
{
    public class GetAllBookmarksQueryHandler : IRequestHandler<GetAllBookmarksQuery, IEnumerable<BookmarkDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllBookmarksQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<BookmarkDto>> Handle(GetAllBookmarksQuery request, CancellationToken cancellationToken)
        {
            var bookmarks = await _unitOfWork.Bookmarks.GetAllAsync();
            var dtos = bookmarks.Select(b => new BookmarkDto
            {
                BookmarkId = b.BookmarkId,
                CollectionId = b.CollectionId,
                Name = b.Name,
                Url = b.Url,
                Icon = b.Icon,
                IsFav = b.IsFav,
                CreatedDate = b.CreatedDate,
                CreatedBy = b.CreatedBy,
                CollectionName = b.Collection != null ? b.Collection.Name : string.Empty
            });
            return dtos;
        }
    }
} 