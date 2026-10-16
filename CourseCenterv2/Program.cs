
using CourseCenterv2.Application.Interfaces;
using CourseCenterv2.ExternalServices;
using CourseCenterv2.Infrastructure.Data;
using CourseCenterv2.Infrastructure.Email;
using CourseCenterv2.Infrastructure.Repositories;
using CourseCenterv2.Services;
using CourseCenterv2.Soap;
using Microsoft.EntityFrameworkCore;
using SoapCore;
using Microsoft.AspNetCore.Builder;


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

// SOAP Registration
builder.Services.AddSoapCore();
builder.Services.AddScoped<
    ICourseCenterSoapService,
    CourseCenterSoapService>();

builder.Services.Configure<EmailSettings>(
    builder.Configuration.GetSection("EmailSettings"));

builder.Services.AddTransient<IEmailService, EmailService>();

builder.Services.AddHttpClient<JsonPlaceholderService>(client =>
{
    client.BaseAddress = new Uri(
        "https://jsonplaceholder.typicode.com/");

    client.Timeout = TimeSpan.FromSeconds(30);
});

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

// SOAP Endpoint
((IApplicationBuilder)app).UseSoapEndpoint<ICourseCenterSoapService>(
    "/CourseCenterService.svc",
    new SoapEncoderOptions(),
    SoapSerializer.DataContractSerializer);

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Student}/{action=Index}/{id?}");

app.Run();

//mohammed
