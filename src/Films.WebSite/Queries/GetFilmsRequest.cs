using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Films.Website.Domain;
using Films.WebSite.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FilmsLibrary.Queries
{
    public class GetFilmsRequest : IRequest<IEnumerable<Film>>
    {

        public GetFilmsRequest(int pageSize, int offset)
        {
            PageSize = pageSize;
            Offset = offset;
        }

        public int PageSize { get; }
        public int Offset { get; }

        public class GetFilmsRequestHandler : IRequestHandler<GetFilmsRequest, IEnumerable<Film>>
        {
            private readonly DataContext context;

            public GetFilmsRequestHandler(DataContext context)
            {
                this.context = context;
            }

            public async Task<IEnumerable<Film>> Handle(GetFilmsRequest request, CancellationToken cancellationToken)
            {
                return await context.Films
                    .AsNoTracking()
                    .Skip(request.Offset)
                    .Take(request.PageSize)
                    .OrderBy(f => f.Title)
                    .ToListAsync(cancellationToken);
            }
        }
    }
}
