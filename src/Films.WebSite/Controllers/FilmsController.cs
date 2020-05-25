using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MediatR;
using FilmsLibrary.Queries;
using System.Threading;
using FilmsLibrary.Commands;
using Films.WebSite.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;

namespace Films.WebSite.Controllers
{
    public class FilmsController : Controller
    {
        private readonly ILogger<FilmsController> logger;
        private readonly IMediator mediator;
        private readonly IAuthorizationService authorizationService;

        public FilmsController(ILogger<FilmsController> logger, IMediator mediator, IAuthorizationService authorizationService)
        {
            this.logger = logger;
            this.mediator = mediator;
            this.authorizationService = authorizationService;
        }

        #region View films
        public async Task<IActionResult> Index(GetFilmsRequest request, CancellationToken cancellationToken)
        {
            return View(await mediator.Send(request, cancellationToken));
        }
        #endregion

        #region View film details
        public async Task<IActionResult> Details(int id, CancellationToken cancellationToken)
        {
            return View(await mediator.Send(new GetFilmByIdRequest(id), cancellationToken));
        } 
        #endregion

        #region Create film

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create(CreateFilmRequest request, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return View(request);
            }

            var id = await mediator.Send(request, cancellationToken);
            return RedirectToActionPermanent(nameof(Details), new { Id = id });
        } 
        #endregion

        #region Edit film
        [Authorize]
        public async Task<IActionResult> Edit(int id, CancellationToken cancellationToken)
        {
            var authorizationResult = await authorizationService.AuthorizeAsync(User, id, "IsOwner");
            if(!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            return View(await mediator.Send(new GetFilmByIdRequest(id), cancellationToken));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(EditFilmRequest request, CancellationToken cancellationToken)
        {
            var authorizationResult = await authorizationService.AuthorizeAsync(User, request.Id, "IsOwner");
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            if(!ModelState.IsValid)
            {
                return View(request);
            }


            await mediator.Send(request, cancellationToken);

            return RedirectToActionPermanent(nameof(Details), new { request.Id });
        }
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
