using System;
using System.Threading;
using System.Threading.Tasks;

using Films.Website.Domain;
using Films.WebSite.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FilmsLibrary.Commands
{
    public class EditFilmRequest : IRequest<Film>
    {
        public int FilmId { get; set; }

        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public int Year { get; set; }

        public string Director { get; set; }

        public class EditFilmRequestHandler : IRequestHandler<EditFilmRequest, Film>
        {
            private readonly DataContext context;

            public EditFilmRequestHandler(DataContext context)
            {
                this.context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public async Task<Film> Handle(EditFilmRequest request, CancellationToken cancellationToken)
            {
                var film = await context.Films.SingleOrDefaultAsync(f => f.Id == request.FilmId, cancellationToken);

                if (film is null)
                {
                    // TODO: do something?
                    return null;
                }

                // We may use Object mapper if there are many properties.
                film.Title = request.Title;
                film.Description = request.Description;
                film.ReleaseYear = request.Year;
                film.Director = request.Director;

                // If we want to update only some specific columns/properties
                // we may use DbContext.Entry(<tracked-entity>).Property(e => e.<Some-Property>).IsModified = true
                // or .Collection(e => e.<Some-Collection>).IsModifed = true
                //
                // example: Update only title property
                // context.Entry(film).Property(e => e.Title).IsModified = true;
                //
                // but for simplicity, i decided to user DbContext.Update()
                // this will issue an update statement for all entity columns
                try
                {
                    context.Update(film);
                    await context.SaveChangesAsync();
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
