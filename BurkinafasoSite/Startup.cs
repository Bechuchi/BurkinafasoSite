﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BurkinafasoSite.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using System.Globalization;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using BurkinafasoSite.Resources;
using BurkinafasoSite.Models;

namespace BurkinafasoSite
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            //*******************
            //Localization: Andrew
            //services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });           

            services.AddMvc()
                //*******************
                //Localization: Andrew
                //.AddViewLocalization(
                //   LanguageViewLocationExpanderFormat.Suffix,
                //   opts => { opts.ResourcesPath = "Resources"; })


                //*******************
                //Localization: "Less than 5min"
                .AddDataAnnotationsLocalization(options =>
                    options.DataAnnotationLocalizerProvider = (type, factory) =>
                        factory.Create(typeof(SharedResources)))

                .SetCompatibilityVersion(CompatibilityVersion.Version_2_1)

                //************************************
                //Localization FLASHBACK
                .AddViewLocalization(o => o.ResourcesPath = "Resources");

            //*******************
            //Localization: "Less than 5min"
            services.AddScoped<SharedViewLocalizer>();

            //*******************
            //Localization: Andrew
            //.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix, opts => { opts.ResourcesPath = "Resources"; })
            //.AddDataAnnotationsLocalization();

            //***********************
            //Localization: TEST MATTEO
            //var supportedCultures = new[]
            //{
            //    new CultureInfo("en-US"),
            //    new CultureInfo("de-CH"),
            //};

            //***********************
            //Localization: TEST MATTEO
            services.Configure<RequestLocalizationOptions>(options =>
            {
                //options.DefaultRequestCulture = new RequestCulture(culture: "de-CH", uiCulture: "de-CH");
                //options.SupportedCultures = supportedCultures;
                //options.SupportedUICultures = supportedCultures;
                options.SupportedCultures.Add(new CultureInfo("en-US"));
                options.SupportedCultures.Add(new CultureInfo("fr"));

                options.SupportedUICultures.Add(new CultureInfo("en-US"));
                options.SupportedUICultures.Add(new CultureInfo("fr"));

                //options.RequestCultureProviders.Clear(); // Clears all the default culture providers from the list
                //options.RequestCultureProviders.Add(new UserProfileRequestCultureProvider()); // Add your custom culture provider back to the list
            });

            //*******************
            //Localization: Andrew (För att välja språk)
            //services.Configure<RequestLocalizationOptions>(
            //    opts =>
            //    {
            //        var supportedCultures = new List<CultureInfo>
            //        {
            //            new CultureInfo("en-GB"),
            //            new CultureInfo("en-US"),
            //            new CultureInfo("en"),
            //            new CultureInfo("fr-FR"),
            //            new CultureInfo("fr"),
            //            new CultureInfo("sv")
            //        };

            //        opts.DefaultRequestCulture = new RequestCulture("en-GB");
            //        // Formatting numbers, dates, etc.
            //        opts.SupportedCultures = supportedCultures;
            //        // UI strings that we have localized.
            //        opts.SupportedUICultures = supportedCultures;
            //    });

            //*******************
            //Identity
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            //*******************
            //Localization: Andrew (För att välja språk)
            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
