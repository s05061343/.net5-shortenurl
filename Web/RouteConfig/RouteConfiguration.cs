using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Web.RouteConfig
{
    public static class RouteConfiguration
    {
        public static void Register(IEndpointRouteBuilder endpoints)
        {
            endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "/home");
        }
    }
}
