using Microsoft.EntityFrameworkCore;
using Vehicle_Mono_BL.Interface;
using Vehicle_Mono_BL.Service;
using Vehicle_Mono_DAL;
using Vehicle_Mono_DAL.Interface;
using Vehicle_Mono_DAL.MODELS;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<MonoVehicleContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("Vehicle_Mono"),
optionsBuilder => optionsBuilder.MigrationsAssembly("Vehicle-Mono-DAL")));

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IMakeVehicle, MakeVehicleService>();
builder.Services.AddScoped<IModelVehicle, ModelVehicleService>();

builder.Services.AddScoped<IMake, Makes>();
// Add services to the container.
//builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

 

app.Run();
