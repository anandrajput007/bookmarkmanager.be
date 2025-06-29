using BookmarkManager.Application.Dto.Collections;
using BookmarkManager.Application.Dto.Bookmarks;
using BookmarkManager.Domain.Interfaces;
using MediatR;

namespace BookmarkManager.Application.Queries.Collections
{
    public class GetAllCollectionsQueryHandler : IRequestHandler<GetAllCollectionsQuery, IEnumerable<CollectionDto>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllCollectionsQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<CollectionDto>> Handle(GetAllCollectionsQuery request, CancellationToken cancellationToken)
        {
            var collections = await _unitOfWork.Collections.GetAllAsync();
            var dtos = collections.Select(c => new CollectionDto
            {
                CollectionId = c.CollectionId,
                Name = c.Name,
                Icon = c.Icon,
                IsFav = c.IsFav,
                CreatedDate = c.CreatedDate,
                CreatedBy = c.CreatedBy,
                BookmarkCount = c.Bookmarks != null ? c.Bookmarks.Count : 0,
                Bookmarks = c.Bookmarks?.Select(b => new BookmarkDto
                {
                    BookmarkId = b.BookmarkId,
                    CollectionId = b.CollectionId,
                    Name = b.Name,
                    Url = b.Url,
                    Icon = b.Icon,
                    IsFav = b.IsFav,
                    CreatedDate = b.CreatedDate,
                    CreatedBy = b.CreatedBy
                }).ToList() ?? new List<BookmarkDto>()
            });
            return dtos;
        }
    }
} 