using BookmarkManager.Application.Dto.Collections;
using BookmarkManager.Domain.Entities;
using BookmarkManager.Domain.Interfaces;
using MediatR;

namespace BookmarkManager.Application.Commands.Collections
{
    public class CreateCollectionCommandHandler : IRequestHandler<CreateCollectionCommand, CollectionDto>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateCollectionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CollectionDto> Handle(CreateCollectionCommand request, CancellationToken cancellationToken)
        {
            var collection = new Collection
            {
                Name = request.Name,
                Icon = request.Icon,
                IsFav = request.IsFav,
                CreatedDate = DateTime.UtcNow,
                CreatedBy = 1
            };
            await _unitOfWork.Collections.AddAsync(collection);
            await _unitOfWork.CommitAsync(cancellationToken);
            
            // Get the created collection with bookmark count for mapping
            var createdCollection = await _unitOfWork.Collections.GetByIdAsync(collection.CollectionId);
            if (createdCollection == null)
            {
                throw new InvalidOperationException("Failed to retrieve created collection");
            }
            
            return new CollectionDto
            {
                CollectionId = createdCollection.CollectionId,
                Name = createdCollection.Name,
                Icon = createdCollection.Icon,
                IsFav = createdCollection.IsFav,
                CreatedDate = createdCollection.CreatedDate,
                CreatedBy = createdCollection.CreatedBy,
                BookmarkCount = createdCollection.Bookmarks != null ? createdCollection.Bookmarks.Count : 0
            };
        }
    }
} 