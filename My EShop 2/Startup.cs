using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using My_EShop_2.Models.Context;
using My_EShop_2.Repositories;
using My_EShop_2.Repositories.Services;

namespace My_EShop_2
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddRazorPages();
            #region DBContext
            services.AddDbContext<MyEShop_DB>(option => option.UseSqlServer(Configuration.GetConnectionString("MaminShopServer")));
            #endregion
            #region Ioc
            services.AddScoped<CategoryRepository, CategoryRepository>();
            services.AddScoped<ProductRepository, ProductRepository>();
            #endregion
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options => { options.LoginPath = "/Account/Login"; options.ExpireTimeSpan = TimeSpan.FromDays(32); options.LogoutPath = "/Account/LogOut"; options.ReturnUrlParameter = "returnUrl"; });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.Use(async (context, next) =>
            {
                if (context.Request.Path.StartsWithSegments("/Admin"))
                {
                    if (!context.User.Identity.IsAuthenticated || !context.User.IsInRole("Admin"))
                    {
                        if (!context.User.Identity.IsAuthenticated)
                        {
                            context.Response.Redirect("/account/login");
                        }
                        else
                        {
                            context.Response.Redirect("/NotFound");
                        }
                    }
                }
                await next.Invoke();

            });
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                //endpoints.MapControllerRoute(
                //    name: "Errors",
                //    pattern: "{notFoundPage}",
                //    defaults: new {controller= "Errors", action="NotFound"}
                //    );
            });
            //app.UseStatusCodePages(new StatusCodePagesOptions()
            //{
            //    HandleAsync = async context =>
            //    {
            //        if (context.HttpContext.Response.StatusCode == 404)
            //        {
            //            context.HttpContext.Response.Redirect($"/Error/NotFound/{context.HttpContext.Request.Path}");
            //        }
            //    }
            //});

            //app.Use(async (context, next) =>
            //{
            //    if (!context.User.Identity.IsAuthenticated || !context.User.IsInRole("Admin"))
            //    {
            //        context.Response.StatusCode = 404;
            //    }
            //    else
            //    {
            //        await next.Invoke();
            //    }
            //});

        }
    }
}

