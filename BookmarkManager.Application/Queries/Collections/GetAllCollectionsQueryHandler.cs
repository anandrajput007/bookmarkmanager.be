using BookmarkManager.Application.Dto.Collections;
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
                BookmarkCount = c.Bookmarks != null ? c.Bookmarks.Count : 0
            });
            return dtos;
        }
    }
} 