using System;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

using Films.Website.Domain;
using Films.WebSite.Data;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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

        [FromForm]
        [Required]
        public IFormFile Poster { get; set; }

        public class CreateFilmRequestHandler : IRequestHandler<CreateFilmRequest, int>
        {
            private readonly DataContext context;
            private readonly IHttpContextAccessor httpContextAccessor;

            public CreateFilmRequestHandler(DataContext context, IHttpContextAccessor httpContextAccessor)
            {
                this.context = context ?? throw new ArgumentNullException(nameof(context));
                this.httpContextAccessor = httpContextAccessor;
            }

            public async Task<int> Handle(CreateFilmRequest request, CancellationToken cancellationToken)
            {
                var film = new Film
                {
                    Title = request.Title,
                    Description = request.Description,
                    ReleaseYear = request.Year,
                    Director = request.Director,

                    CreatorId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)
                };

                // Process film poster
                using (var imageStream = new MemoryStream())
                using (var uploadedImageStream = request.Poster.OpenReadStream())
                {
                    await uploadedImageStream.CopyToAsync(imageStream);

                    film.Image = imageStream.ToArray();
                }

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
