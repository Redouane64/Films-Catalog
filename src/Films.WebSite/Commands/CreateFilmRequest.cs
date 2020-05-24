using System;
using System.Threading;
using System.Threading.Tasks;

using Films.Website.Domain;
using Films.WebSite.Data;

using MediatR;

namespace FilmsLibrary.Commands
{
    public class CreateFilmRequest : IRequest<Film>
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Director { get; set; }

        public int Year { get; set; }

        public class CreateFilmRequestHandler : IRequestHandler<CreateFilmRequest, Film>
        {
            private readonly DataContext context;

            public CreateFilmRequestHandler(DataContext context)
            {
                this.context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public async Task<Film> Handle(CreateFilmRequest request, CancellationToken cancellationToken)
            {
                var film = new Film
                {
                    Title = request.Title,
                    Description = request.Description,
                    ReleaseYear = request.Year,
                    Director = request.Director,

                    // TODO: set creator.
                };

                try
                {
                    context.Add(film);
                    await context.SaveChangesAsync(cancellationToken);
                }
                catch (Exception)
                {
                    // TODO: Exceptions.
                    throw;
                }

                return film;
            }
        }
    }
}
