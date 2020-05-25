using System;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

using Films.Website.Domain;
using Films.WebSite.Data;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace FilmsLibrary.Commands
{
    public class EditFilmRequest : IRequest
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        public int Year { get; set; }

        [Required]
        public string Director { get; set; }

        public class EditFilmRequestHandler : IRequestHandler<EditFilmRequest>
        {
            private readonly DataContext context;

            public EditFilmRequestHandler(DataContext context)
            {
                this.context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public async Task<Unit> Handle(EditFilmRequest request, CancellationToken cancellationToken)
            {
                var film = await context.Films.SingleOrDefaultAsync(f => f.Id == request.Id, cancellationToken);

                if (film is null)
                {
                    // TODO: do something?
                    return await Task.FromCanceled<Unit>(cancellationToken);
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

                return await Unit.Task;
            }
        }
    }
}
