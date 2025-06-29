using BookmarkManager.Application.Dto.Bookmarks;
using BookmarkManager.Domain.Entities;
using BookmarkManager.Domain.Interfaces;
using MediatR;

namespace BookmarkManager.Application.Commands.Bookmarks
{
    public class CreateBookmarkCommandHandler : IRequestHandler<CreateBookmarkCommand, BookmarkDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBookmarkCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BookmarkDto> Handle(CreateBookmarkCommand request, CancellationToken cancellationToken)
        {
            var bookmark = new Bookmark
            {
                CollectionId = request.CollectionId,
                Name = request.Name,
                Url = request.Url,
                Icon = request.Icon,
                IsFav = request.IsFav,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            };
            await _unitOfWork.Bookmarks.AddAsync(bookmark);
            await _unitOfWork.CommitAsync(cancellationToken);
            
            // Get the created bookmark with collection info for mapping
            var createdBookmark = await _unitOfWork.Bookmarks.GetByIdAsync(bookmark.BookmarkId);
            if (createdBookmark == null)
            {
                throw new InvalidOperationException("Failed to retrieve created bookmark");
            }
            
            return new BookmarkDto
            {
                BookmarkId = createdBookmark.BookmarkId,
                CollectionId = createdBookmark.CollectionId,
                Name = createdBookmark.Name,
                Url = createdBookmark.Url,
                Icon = createdBookmark.Icon,
                IsFav = createdBookmark.IsFav,
                CreatedDate = createdBookmark.CreatedDate,
                CreatedBy = createdBookmark.CreatedBy,
                CollectionName = createdBookmark.Collection != null ? createdBookmark.Collection.Name : string.Empty
            };
        }
    }
} 