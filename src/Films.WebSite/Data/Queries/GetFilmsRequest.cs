﻿using System;
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

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmsLibrary.Queries
{
    public class GetFilmsRequest : IRequest<ItemsPage<FilmViewModel>>
    {

        [FromQuery]
        public int? PageSize { get; set; }

        [FromQuery]
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
                var total = await context.Films.CountAsync(cancellationToken);

                if (request.PageSize > total)
                {
                    request.PageSize = total;
                }

                var items = context.Films
                    .AsNoTracking();

                if (total > 0)
                {
                    items = items.Skip(request.Offset.Value)
                                 .Take(request.PageSize.Value);
                }

                return new ItemsPage<FilmViewModel>(
                    await items
                        .OrderBy(f => f.Title)
                        .ProjectTo<FilmViewModel>(mapper.ConfigurationProvider)
                        .ToListAsync(cancellationToken), 
                    total
                ) {
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
