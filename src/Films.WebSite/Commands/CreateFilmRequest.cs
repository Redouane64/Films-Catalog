using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Films.WebSite.Data;

using MediatR;

namespace FilmsLibrary.Commands
{
    public class CreateFilmRequest : IRequest
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public string Director { get; set; }

        public int Year { get; set; }

        public class CreateFilmRequestHandler : IRequestHandler<CreateFilmRequest>
        {
            private readonly DataContext context;

            public CreateFilmRequestHandler(DataContext context)
            {
                this.context = context;
            }

            public Task<Unit> Handle(CreateFilmRequest request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}
