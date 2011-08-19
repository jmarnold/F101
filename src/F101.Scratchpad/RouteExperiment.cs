using System.Web;
using System.Web.Routing;
using FubuMVC.Core.Registration.Nodes;

namespace F101.Scratchpad
{
    public class RouteExperiment : IRouteHandler
    {
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            // parse some routedata 
            // requestContext.RouteData.Values["controller"]
            return new HttpHandlerExperiment(null);
        }
    }

    public class HttpHandlerExperiment : IHttpHandler
    {
        private readonly BehaviorChain _chain;

        public HttpHandlerExperiment(BehaviorChain chain)
        {
            _chain = chain;
        }

        public void ProcessRequest(HttpContext context)
        {
        }

        public bool IsReusable
        {
            get { return false; }
        }
    }
}