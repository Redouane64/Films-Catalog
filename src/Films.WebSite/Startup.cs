using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Films.WebSite.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Films.Website.Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using MediatR;
using AutoMapper;
using FilmsLibrary.Options;
using FilmsLibrary.Filters;
using Microsoft.AspNetCore.Authorization;
using Films.WebSite.Infrastructure;
using Microsoft.AspNetCore.ResponseCompression;

namespace Films.WebSite
{
    public class Startup
    {
        private readonly IWebHostEnvironment environment;

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            this.environment = environment;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("Default")));

            services.AddControllersWithViews(options => {
                options.Filters.Add<PagingFilter>();
            });

            services.AddRazorPages();

            services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultScheme = IdentityConstants.ApplicationScheme;
            })
            .AddCookie(IdentityConstants.ApplicationScheme,
                options => {
                    options.Cookie.Name = "Films-Library";
                    options.ClaimsIssuer = "Films-Library";
                    options.LoginPath = "/Account/Login";
                    options.LogoutPath = "/Account/LogOut";
                        
                });

            services.AddAuthorization(options => {
                options.AddPolicy("IsOwner", config =>
                {
                    config.RequireAuthenticatedUser();
                    config.AddRequirements(new IsOwnerAuthorizationRequirement());
                });
            });

            services.AddIdentityCore<User>(options =>
            {
                if(environment.IsDevelopment())
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                }
            })
            .AddUserManager<UserManager<User>>()
            .AddSignInManager<SignInManager<User>>()
            .AddEntityFrameworkStores<DataContext>();

            services.AddMediatR(typeof(Startup));
            services.AddAutoMapper(typeof(Startup));

            services.Configure<DefaultPagingOptions>(Configuration.GetSection(nameof(DefaultPagingOptions)));

            services.AddScoped<IAuthorizationHandler, IsOwnerAuthorizationHandler>();

            services.AddResponseCaching();
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<BrotliCompressionProvider>();
                options.Providers.Add<GzipCompressionProvider>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Films/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseStatusCodePages();
            app.UseRouting();

            app.UseResponseCompression();
            app.UseResponseCaching();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Films}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
