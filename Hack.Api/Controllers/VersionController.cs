using System.Web.Http;

namespace Hack.Api.Controllers
{
    public class VersionController : ApiController
    {
        public string Get()
        {
            return this.GetType().Assembly.GetName().Version.ToString();
        }
    }
}
