using AnimePlace.Core.Contracts;
using AnimePlace.Core.Models.Account;
using AnimePlace.Core.Services;
using AnimePlace.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AnimePlaceDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

//builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
//.AddEntityFrameworkStores<AnimePlaceDbContext>();

builder.Services.AddDefaultIdentity<ApplicationUser>(options =>
{
    options.Password.RequiredLength = 5;

    options.User.RequireUniqueEmail = true;
})
    .AddEntityFrameworkStores<AnimePlaceDbContext>();

builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IAnimeService, AnimeService>();
builder.Services.AddHttpClient("AnimeApi", httpClient =>
{
    httpClient.BaseAddress = new Uri("https://gogoanime.herokuapp.com/");
    httpClient.DefaultRequestHeaders.Add(
        HeaderNames.Accept, "application/json");
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
