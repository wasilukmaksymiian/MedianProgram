using MedianProgram.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

//konfiguracja connection string
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

//konfiguracja sqlite
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlite(connectionString));

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

//migracja
using(var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=MedianModels}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
