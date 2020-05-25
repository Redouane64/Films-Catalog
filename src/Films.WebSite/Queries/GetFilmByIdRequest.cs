using System;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using Films.Website.Domain;
using Films.WebSite.Data;
using Films.WebSite.Models.ViewModels;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FilmsLibrary.Queries
{
    public class GetFilmByIdRequest : IRequest<FilmDetailsViewModel>
    {
        public int Id { get; }

        public GetFilmByIdRequest(int id)
        {
            Id = id;
        }

        public class GetFilmByIdRequestHandler : IRequestHandler<GetFilmByIdRequest, FilmDetailsViewModel>
        {
            private readonly DataContext context;
            private readonly IMapper mapper;

            public GetFilmByIdRequestHandler(DataContext context, IMapper mapper)
            {
                this.context = context ?? throw new ArgumentNullException(nameof(context));
                this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            }

            public async Task<FilmDetailsViewModel> Handle(GetFilmByIdRequest request, CancellationToken cancellationToken)
            {
                var film = await context.Films
                    .AsNoTracking()
                    .SingleOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

                return mapper.Map<FilmDetailsViewModel>(film);
            }
        }

        public class FilmDetailsMappingProfile : Profile
        {
            public FilmDetailsMappingProfile()
            {
                CreateMap<Film, FilmDetailsViewModel>()
                    .ForMember(dest => dest.Year, options => options.MapFrom(source => source.ReleaseYear));
            }
        }
    }
}
