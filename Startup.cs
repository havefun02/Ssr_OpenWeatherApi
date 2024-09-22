using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using AutoMapper;
using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Serilog;
using App.Core;
using Serilog.Core;
using App.AppInterfaces;
using App.Services;
namespace App;

public class Startup
{
    private readonly IConfiguration _configuration;
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>();
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IPaginationService<>), typeof(OffsetPaginationService<>));
        services.AddScoped(typeof(IFilterService<>), typeof(FilterService<>));
        services.AddScoped(typeof(ISortingService<>), typeof(SortingService<>));
        services.AddScoped<IAppService, AppService>();
        services.AddHttpClient();

        services.AddSingleton<IMapper>(provider =>
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            return configuration.CreateMapper();
        });
        services.AddCors(options =>
        {
            options.AddPolicy("AllowAll", policy =>
            {
                policy.AllowAnyOrigin()
                      .AllowAnyHeader()
                      .AllowAnyMethod();
            });
        });
        services.AddMvc();
        services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo { Title = "Weather API", Version = "v1" }); });
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "App API V1");
            c.RoutePrefix =string.Empty; // Set Swagger UI at the root
        });
        app.UseCors("AllowAll");
        app.UseMiddleware<LoggingMiddleware>();
        app.UseRouting();
        app.UseDefaultFiles();
        app.UseStaticFiles();
        app.UseStatusCodePages(async context =>
        {
            var response = context.HttpContext.Response;
            var statusCode = response.StatusCode;

            if (statusCode == 404)
            {
                response.ContentType = "text/html";
                await response.SendFileAsync("wwwroot/notFoundPage.html");
            }
        });
        
        app.UseEndpoints(endpoints =>
        {
            Log.Information("App started on port 3000");
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller}/{action}/{id?}");
        });
    }
}