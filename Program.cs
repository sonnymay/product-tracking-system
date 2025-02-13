using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using MySimpleWebApp.Data;

var builder = WebApplication.CreateBuilder(args);

// Add Entity Framework Core with SQLite.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add ASP.NET Identity.
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add logging
builder.Logging.AddConsole();
builder.Logging.AddDebug();

// Add CORS
builder.Services.AddCors();

// Add health checks
builder.Services.AddHealthChecks();

// Optionally add other services such as Swagger, health checks, etc.
// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // Use a custom error page and HSTS in production.
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    // Use the developer exception page in development.
    app.UseDeveloperExceptionPage();
    // Optionally expose Swagger in development.
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseCors(options => options
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapHealthChecks("/health");

app.Run();
