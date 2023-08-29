using DataAccessLayer.DataServices;
using DataAccessLayer.Models;
using BusinessLogicLayer.LogicServices;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddDbContext<ValentinTuiDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("constring")));
builder.Services.AddScoped<IFlightDataDAL, FlightDataDAL>();
builder.Services.AddScoped<IAirportDataDAL, AirportDataDAL>();
builder.Services.AddScoped<IFlightLogic, FlightLogic>();
builder.Services.AddScoped<IAirportLogic, AirportLogic>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
