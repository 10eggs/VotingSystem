using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VotingSystem.Application;
using VotingSystem.Database;
using VotingSystem.Models;

namespace VotingSystem.Ui
{
    public class Startup
    {
        //How you get your main logic into the middleware
        public void ConfigureServices(IServiceCollection services)
        {
            //We are able to add middleware in this way, but when we are using MapDefaultControllerRoute() we should add our controllers to the services
            //services.AddSingleton<Service201>();

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseInMemoryDatabase("Database");
            });


            //When you are service which rely on db context it should be scoped context
            services.AddSingleton<IVotingPollFactory, VotingPollFactory>();
            services.AddSingleton<ICounterManager, CounterManager>();
            services.AddScoped<StatisticsInteractor>();
            services.AddScoped<VotingInteractor>();
            services.AddScoped<VotingPollInteractor>();
            services.AddScoped<IVotingPollPersistance, VotingSystemPersistance>();
            services.AddControllersWithViews();
            services.AddRazorPages();


        }
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //To avoid request for favicon.ico
            app.UseStaticFiles();

            //This line is added to provide https
            app.UseHttpsRedirection();

            app.UseRouting();

            //Just an example
            //app.UseMiddleware<CustomMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                //Opt to MVC rather than specifying all particular routes
                endpoints.MapRazorPages();
                endpoints.MapDefaultControllerRoute();
                //endpoints.MapGet("/", async context =>
                //{
                //    context.Response.StatusCode = 201;
                //    await context.Response.WriteAsync("Hello World!");
                //});
            });
        }
    }

    //public class Service201
    //{
    //    public int GetCode() => 201;
    //}


    //Example of middleware
    //public class CustomMiddleware
    //{
    //    private readonly RequestDelegate _request;
    //    private readonly Service201 _service;

    //    public CustomMiddleware(RequestDelegate request,Service201 service)
    //    {
    //        this._request = request;
    //        this._service = service;
    //    }

    //    public async Task Invoke(HttpContext context)
    //    {
    //        //request is coming in
    //        context.Response.StatusCode = _service.GetCode();
    //        context.Response.ContentType = "application/json";

    //        //request is coming out
    //        await _request(context);
    //    }
    //}
}
