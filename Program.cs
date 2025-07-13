using BarberSalonPrototype.Models;
using BarberSalonPrototype.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Register application services
builder.Services.AddScoped<IStaffService, StaffService>();
builder.Services.AddScoped<IServiceService, ServiceService>();
builder.Services.AddScoped<IBookingService, BookingService>();

// Add logging
builder.Logging.AddConsole();
builder.Logging.AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Custom error handling for 404
app.Use(async (context, next) =>
{
    await next();
    
    if (context.Response.StatusCode == 404)
    {
        context.Request.Path = "/Home/NotFound";
        await next();
    }
});

app.Run(); 