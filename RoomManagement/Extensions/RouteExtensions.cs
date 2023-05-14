namespace RoomManagement.Extensions
{
    public static class RouteExtensions
    {
        // Định nghĩa route template, route constraint cho các
        // endpoints kết hợp với các action trong các controller.
        public static IEndpointRouteBuilder UseRoomManagementRoutes(this IEndpointRouteBuilder endpoints)
        {

            endpoints.MapControllerRoute(
              name: "areas",
              pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
            );


            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
              );

            return endpoints;
        }
    }
}
