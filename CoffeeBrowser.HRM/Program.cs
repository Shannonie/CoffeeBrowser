using CoffeeBrowser.HRM.Data;
using CoffeeBrowser.HRM.Shared;
using Microsoft.EntityFrameworkCore;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

#region Dependency Injection
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

/* Register DbContext with option string
 * builder.Services.AddDbContext<EmployeeHRMDbContext>(
 * option => option.UseSqlServer("Data Source=(localDb)" +
 * "\\MSSQLLocalDb; Initial Catalog=EmployeeHRMDb"));*/
/* Register DbContext with appsettings.json configuration*/
builder.Services.AddDbContextFactory<EmployeeHRMDbContext>(
    opt => opt.UseSqlServer(
        builder.Configuration.GetConnectionString("EmployeeHRMDb")));
builder.Services.AddHttpClient();
builder.Services.AddCascadingAuthenticationState();
//AddScoped:used for an scope of HTTP request
builder.Services.AddScoped<StateContainer>();
#endregion

WebApplication app = builder.Build();

#region To Make Sure It's Latest Database-Migration
// Don't do this in production, just useful for development
await EnsureDatabaseIsMigrated(app.Services);
async Task EnsureDatabaseIsMigrated(IServiceProvider services)
{
    using var scope = services.CreateScope();
    using var ctx = scope.ServiceProvider.GetService<EmployeeHRMDbContext>();
    if (ctx is not null)
    {
        await ctx.Database.MigrateAsync();
    }
}
#endregion

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
