using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace Web.RouteConfig
{
    public static class RouteConfiguration
    {
        public static void Register(IEndpointRouteBuilder endpoints)
        {
            //endpoints.MapControllerRoute(
            //        name: "default",
            //        pattern: "{controller}/{action=Index}/{id?}");

            endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "/home");

            //endpoints.MapControllerRoute(
            //        name: "v1",
            //        pattern: "{controller}/{v1}/{action}/{id?}");
        }
    }
}
