using RedisApp.Web.Services;

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
| REDIS DISTRIBUTED CACHE
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

/* 
|-------------------------------------------------------
| REDIS STACK EXCHANGE
|------------------------------------------------------- 
|
| To be able to use Redis Stack Exchange, we should 
| create an istance of the Redis Service.
|
| It is important to create this instance as singleton.
| We will only need one instance of Redis.
|
*/

builder.Services.AddSingleton<RedisService>();

/* 
|
| MIGRATIONS
|
*/

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
app.MapControllerRoute(name: "stackexchange", pattern: "StackExchange/{controller=StringType}/{action=Set}/{id?}");

app.Run(); 
