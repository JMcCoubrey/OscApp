﻿using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Osc.Db;
using Microsoft.AspNetCore.Http;
using System.IO;
using Audit.Core;
using System.Security.Claims;
using Newtonsoft.Json;
using OscApp.Web.Services;
using OscApp.DAL.Implementation;
using OscApp.DAL;

namespace OscApp.Web
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddEntityFrameworkSqlServer();

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
            services.AddTransient<ITenancyRepository, TenancyRepository>();
            services.AddTransient<ITrainerRepository, TrainerRepository>();

            services.AddMultitenancy<Tenancy, CachingTenantResolver>();

			services.AddMvc().AddJsonOptions(options =>
			{
				options.SerializerSettings.Formatting = Formatting.None;
				options.SerializerSettings.PreserveReferencesHandling = PreserveReferencesHandling.Objects;
			});
		}

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMultitenancy<Tenancy>();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}