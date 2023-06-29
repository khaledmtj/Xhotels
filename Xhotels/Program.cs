using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using Xhotels.Business.Services;
using Xhotels.Data;
using Xhotels.Data.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HotelContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default"));
});

builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IReservationRepository, ReservationRepository>();
builder.Services.AddTransient<IRoomRepository, RoomRepository>();
builder.Services.AddTransient<IRoomTypeRepository, RoomTypeRepository>();
builder.Services.AddTransient<IReservationService, ReservationService>();

IConfigurationSection corsSettings = builder.Configuration.GetSection("CorsSettings");
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsSettings["PolicyName"],
        builder => builder
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowAnyOrigin()
        .WithOrigins(
            corsSettings["Host"]
            )
        );
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var hotelContextService = serviceProvider.GetRequiredService<HotelContext>();
    if (hotelContextService.RoomTypes.Count() == 0)
    {
        string roomTypesFile = System.IO.File.ReadAllText("roomTypes.json");
        var roomTypes = JsonSerializer.Deserialize<List<Xhotels.Data.Models.RoomType>>(roomTypesFile);
        hotelContextService.AddRange(roomTypes);
        hotelContextService.SaveChanges();
    }

    if (hotelContextService.Rooms.Count() == 0)
    {
        string roomsFile = System.IO.File.ReadAllText("rooms.json");
        var rooms = JsonSerializer.Deserialize<List<Xhotels.Data.Models.Room>>(roomsFile);
        hotelContextService.AddRange(rooms);
        hotelContextService.SaveChanges();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

var corsPolicy = builder.Configuration["CorsSettings:PolicyName"];
app.UseCors(corsPolicy);

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
