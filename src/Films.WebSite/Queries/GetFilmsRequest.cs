using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Films.Website.Domain;
using Films.WebSite.Data;
using Films.WebSite.Models.ViewModels;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FilmsLibrary.Queries
{
    public class GetFilmsRequest : IRequest<IEnumerable<FilmViewModel>>
    {

        public int? PageSize { get; set; }
        public int? Offset { get; set; }

        public class GetFilmsRequestHandler : IRequestHandler<GetFilmsRequest, IEnumerable<FilmViewModel>>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public GetFilmsRequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context ?? throw new ArgumentNullException(nameof(context));
                this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<IEnumerable<FilmViewModel>> Handle(GetFilmsRequest request, CancellationToken cancellationToken)
            {
                return await context.Films
                    .AsNoTracking()
                    .Skip(request.Offset.Value)
                    .Take(request.PageSize.Value)
                    .OrderBy(f => f.Title)
                    .ProjectTo<FilmViewModel>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);
            }
        }

        public class FilmMappingProfile : Profile
        {
            public FilmMappingProfile()
            {
                CreateMap<Film, FilmViewModel>()
                    .ForMember(dest => dest.Year, options => options.MapFrom(source => source.ReleaseYear));
            }
        }
    }
}
