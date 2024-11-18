using Events_v1.Models.Data;
using Events_v1.Models.DomainModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Get the current directory path
string path = Directory.GetCurrentDirectory();

// Configure the DbContext with SQL Server and replace the [DataDirectory] placeholder with the current path
builder.Services.AddDbContext<EventContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DBConnection").Replace("[DataDirectory]", path)));

// Configure Identity services with custom password requirements
builder.Services.AddIdentity<User, IdentityRole>(options =>
{
    options.Password.RequiredLength = 5;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
})
.AddEntityFrameworkStores<EventContext>()
.AddDefaultTokenProviders();

// Add authorization policy for admin users
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("UserIsAdmin", policy =>
    {
        policy.RequireClaim("IsAdmin", "true");
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // Use custom error handling in production
    app.UseExceptionHandler("/Home/Error");
    // Enable HSTS for security
    app.UseHsts();
}

// Enable HTTPS redirection
app.UseHttpsRedirection();
// Enable serving static files
app.UseStaticFiles();

// Enable routing
app.UseRouting();

// Enable authentication
app.UseAuthentication();

// Enable authorization
app.UseAuthorization();

// Configure the default route for controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Run the application
app.Run();
