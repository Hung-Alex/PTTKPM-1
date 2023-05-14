namespace RoomManagement.Extensions
{
    public static class RouteExtensions
    {
        // Định nghĩa route template, route constraint cho các
        // endpoints kết hợp với các action trong các controller.
        public static IEndpointRouteBuilder UseRoomManagementRoutes(this IEndpointRouteBuilder endpoints)
        {

            endpoints.MapControllerRoute(
                name: "admin-area",
                pattern: "admin/{controller=Dashboard}/{action=Index}/{id?}",
                defaults: new { area = "Admin" }
              );


            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
              );

            return endpoints;
        }
    }
}
