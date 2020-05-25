using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

using Films.Website.Domain;
using Films.WebSite.Data;

using MediatR;

namespace FilmsLibrary.Commands
{
    public class CreateFilmRequest : IRequest<int>
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Director { get; set; }

        public int Year { get; set; }

        public class CreateFilmRequestHandler : IRequestHandler<CreateFilmRequest, int>
        {
            private readonly DataContext context;

            public CreateFilmRequestHandler(DataContext context)
            {
                this.context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public async Task<int> Handle(CreateFilmRequest request, CancellationToken cancellationToken)
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

                return film.Id;
            }
        }
    }
}
