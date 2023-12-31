var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

/* 
|-------------------------------------------------------
| MEMORY CACHE
|------------------------------------------------------- 
|
| To be able to use Redis or any other cache service,
| we need to add Memory Cache service to our application.
|
*/

builder.Services.AddMemoryCache();



var app = builder.Build();

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

app.Run();
