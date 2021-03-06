using _0_Framework.Application;
using BlogManagement.Infrastructure.Coonfiguration;
using CommentManagement.Infrastructure.EfCore;
using DiscountManagement.Configuration;
using InventoryManagement.Infrastructure.Configuration;
using ServiceHost;
using ShopManagement.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

var connectionString = builder.Configuration.GetConnectionString("MaralShopDbContext");

#region DependencyBootstrappers
ShopBootstrapper.Configure(builder.Services, connectionString);
DiscountBootstrapper.Configure(builder.Services, connectionString);
InventoryBootstrapper.Configure(builder.Services, connectionString);
BlogBoostrapper.Configure(builder.Services, connectionString);
CommentBootstrapper.Configure(builder.Services, connectionString);
#endregion

builder.Services.AddTransient<IFileUpload,FileUpload>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
