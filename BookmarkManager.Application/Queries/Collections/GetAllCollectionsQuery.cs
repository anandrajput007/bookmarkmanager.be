using BookmarkManager.Application.Dto.Collections;
using MediatR;
using System.Collections.Generic;

namespace BookmarkManager.Application.Queries.Collections
{
    public class GetAllCollectionsQuery : IRequest<IEnumerable<CollectionDto>>
    {
    }
} 