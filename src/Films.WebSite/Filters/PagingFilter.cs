using System.Linq;

using FilmsLibrary.Options;
using FilmsLibrary.Queries;

using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Options;

namespace FilmsLibrary.Filters
{
    public class PagingFilter : IActionFilter
    {
        private readonly DefaultPagingOptions options;

        public PagingFilter(IOptionsSnapshot<DefaultPagingOptions> options)
        {
            this.options = options.Value;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        { }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var pagingOptionsArguments = context.ActionArguments.Values.OfType<GetFilmsRequest>();

            foreach (var argument in pagingOptionsArguments)
            {
                argument.Offset ??= options.Offset;
                argument.PageSize ??= options.Size;
            }
        }
    }
}
