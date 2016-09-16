using System;
using System.Web.Http;
using System.Web.Http.Description;

namespace SimpleCQRS.Controllers
{
    public class HomeController : ApiController
    {
        [HttpGet]
        [Route]
        [ApiExplorerSettings(IgnoreApi = true)]
        public IHttpActionResult Index() =>
            Redirect(new Uri("/swagger/ui/index", UriKind.Relative));
    }
}
