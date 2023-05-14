using Microsoft.EntityFrameworkCore;
using RoomManagement.Data.Context;
using RoomManagement.Extensions;
using RoomManagement.Mapster;
using RoomManagement.Validations;

var builder = WebApplication.CreateBuilder(args);
{
    builder.ConfigureMvc()
           .ConfigureNLog()
           .ConfigureServices()
           .ConfigureMapster()
           .ConfigureFluentValidation();
}

var app = builder.Build();
{
    app.UseRequestPipeline();
    app.UseRoomManagementRoutes();
    app.UseDataSeeder();
}

app.Run();
