var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

/* 
|-------------------------------------------------------
| MEMORY CACHE
|------------------------------------------------------- 
|
| To be able to use Memory cache
| we need to add Memory Cache service to our application.
|
*/

builder.Services.AddMemoryCache();

/* 
|-------------------------------------------------------
| REDIS
|------------------------------------------------------- 
|
| To be able to use Redis cache we need to add 
| AddStackExchangeRedisCache service to our application.
|
*/

builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = "localhost:6379";
});

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
app.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run(); 
