using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Films.WebSite.Data;
using Films.WebSite.Models;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FilmsLibrary.Commands
{
    public class EditFilmRequest : IRequest
    {
        public FilmModel Film { get; }

        public EditFilmRequest(FilmModel film)
        {
            Film = film;
        }

        public class EditFilmRequestHandler : IRequestHandler<EditFilmRequest>
        {
            private readonly DataContext context;

            public EditFilmRequestHandler(DataContext context)
            {
                this.context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public async Task<Unit> Handle(EditFilmRequest request, CancellationToken cancellationToken)
            {
                var film = await context.Films.SingleOrDefaultAsync(f => f.Id == request.Film.Id, cancellationToken);

                if (film is null)
                {
                    // TODO: do something?
                    return await Unit.Task;
                }

                // We may use Object mapper if there are many properties.
                film.Title = request.Film.Title;
                film.Description = request.Film.Description;
                film.ReleaseYear = request.Film.Year;
                film.Director = request.Film.Director;

                if (request.Film.Poster != null)
                {
                    // Process film poster
                    using (var imageStream = new MemoryStream())
                    using (var uploadedImageStream = request.Film.Poster.OpenReadStream())
                    {
                        await uploadedImageStream.CopyToAsync(imageStream);

                        film.Image = imageStream.ToArray();
                    }

                    context.Entry(film).Property(e => e.Image).IsModified = true;
                }

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

                return await Unit.Task;
            }
        }
    }
}
