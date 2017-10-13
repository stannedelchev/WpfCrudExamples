using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace CrudExamples.WebApi.Controllers
{
    /// <summary>
    /// Slows down a request by some milliseconds, given by <see cref="DelayTimeInMilliseconds"/>.
    /// </summary>
    public class ThrottleActionFilterAttribute : ActionFilterAttribute
    {
        public int DelayTimeInMilliseconds { get; set; } = 1000;

        public override async Task OnActionExecutingAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            await Task.Delay(this.DelayTimeInMilliseconds, cancellationToken);
            await base.OnActionExecutingAsync(actionContext, cancellationToken);
        }
    }
}