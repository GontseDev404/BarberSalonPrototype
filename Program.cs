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

// Configure static files with custom content types for SVG placeholders
var provider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
provider.Mappings[".svg"] = "image/svg+xml";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = provider,
    OnPrepareResponse = ctx =>
    {
        // Serve SVG files with .jpg extensions as SVG content
        if (ctx.File.Name.EndsWith(".jpg") && ctx.Context.Request.Path.StartsWithSegments("/images") && ctx.File.PhysicalPath != null)
        {
            var content = System.IO.File.ReadAllText(ctx.File.PhysicalPath);
            if (content.TrimStart().StartsWith("<svg"))
            {
                ctx.Context.Response.ContentType = "image/svg+xml";
            }
        }
    }
});

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