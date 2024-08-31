using Assignment_PRN.Configurations;
using Assignment_PRN.Data;
using Assignment_PRN.Hubs;
using Assignment_PRN.Models;
using Assignment_PRN.Security;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<AssignmentPRNContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DB")));


builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddIdentitySetup(builder.Configuration);
builder.Services.RegisterServices();
builder.Services.AddRazorPages(options => { options.Conventions.AuthorizeAreaFolder("Admin", "/", "AdminOnly"); });
builder.Services.AddSignalR();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
var app = builder.Build();
var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
using var scope = scopeFactory.CreateScope();
var dbInitializer = scope.ServiceProvider.GetRequiredService<IDbInitializer>();
dbInitializer.Initialize();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();
app.MapHub<SignalRHub>("/signalr");
app.Run();

