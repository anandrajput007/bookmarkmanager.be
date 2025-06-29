using BookmarkManager.Application.Dto.Bookmarks;
using MediatR;
using System.Collections.Generic;

namespace BookmarkManager.Application.Queries.Bookmarks
{
    public class GetAllBookmarksQuery : IRequest<IEnumerable<BookmarkDto>>
    {
    }
} 