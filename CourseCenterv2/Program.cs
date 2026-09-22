using CourseCenterv2.Application.Interfaces;
using CourseCenterv2.Infrastructure.Data;
using CourseCenterv2.Infrastructure.Repositories;
using CourseCenterv2.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// EF Core + PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options
        .UseLazyLoadingProxies()
        .UseNpgsql(
            builder.Configuration.GetConnectionString("DefaultConnection")
        ));
// Generic Repository
builder.Services.AddScoped(
    typeof(IGenericRepository<>),
    typeof(GenericRepository<>)
);

// Services
builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<ICourseService, CourseService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=Index}/{id?}");

app.Run();