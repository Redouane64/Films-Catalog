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
    public class GetFilmsRequest : IRequest<ItemsPage<FilmViewModel>>
    {

        public int? PageSize { get; set; }
        public int? Offset { get; set; }

        public class GetFilmsRequestHandler : IRequestHandler<GetFilmsRequest, ItemsPage<FilmViewModel>>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public GetFilmsRequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context ?? throw new ArgumentNullException(nameof(context));
                this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<ItemsPage<FilmViewModel>> Handle(GetFilmsRequest request, CancellationToken cancellationToken)
            {
                var items = await context.Films
                    .AsNoTracking()
                    .Skip(request.Offset.Value)
                    .Take(request.PageSize.Value)
                    .OrderBy(f => f.Title)
                    .ProjectTo<FilmViewModel>(mapper.ConfigurationProvider)
                    .ToListAsync(cancellationToken);

                var total = await context.Films.CountAsync(cancellationToken);

                return new ItemsPage<FilmViewModel>(items, total) 
                { 
                    Offset = request.Offset.Value, 
                    Size = request.PageSize.Value 
                };
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
