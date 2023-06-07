using graduation.Data;
using graduation.Data.infrastructure.Entities;
using Graduation.WebUI.Infrastructure.Cache;
using Graduation.WebUI.Infrastructure.Rules;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Graduation.WebUI.Management
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public IConfigurationRoot ConfigurationRoot { get; }
#pragma warning disable CS0618 // Tür veya üye artýk kullanýlmýyor
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; }
#pragma warning restore CS0618 // Tür veya üye artýk kullanýlmýyor



#pragma warning disable CS0618 // Tür veya üye artýk kullanýlmýyor
        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
#pragma warning restore CS0618 // Tür veya üye artýk kullanýlmýyor

        {
            Configuration = configuration;
            Environment = env;
            ConfigurationRoot = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.(env.EnvironmentName).json", optional: true)
                .AddEnvironmentVariables()
                .Build();


        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsDevelopment())
            {
                services.AddControllersWithViews().AddRazorRuntimeCompilation();

            }
            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));
            services.AddOptions();

            //cache
            services.AddMemoryCache();
            services.AddTransient<ICache, graduation.infrastructure.Caching.Memory.Cache>();
            services.AddTransient<CacheHelper>();



            services.Configure<CookiePolicyOptions>(options =>
            {
                options.MinimumSameSitePolicy = SameSiteMode.Strict;
            });

            services.AddAuthentication(o =>
            {
                o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            })
                .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o => { o.LoginPath = new PathString("/login"); });
            //data
            services.AddTransient<CategoryData>();
            services.AddTransient<ContentData>();
            services.AddTransient<ContentCategoryData>();
            services.AddTransient<ContentTagData>();
            services.AddTransient<TagData>();
            services.AddTransient<MediaData>();
            services.AddTransient<AuthorData>();
            services.AddTransient<SettingData>();
            services.AddTransient<CommentData>();
            services.AddTransient<RolePageData>();
            services.AddTransient<RoleData>();





            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddMvc(x =>
            {
                x.EnableEndpointRouting = false;
            });
            services.Configure<RouteOptions>(routeOptions => routeOptions.AppendTrailingSlash = true);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            RedirectToHttpsWwwNonWwwRule rule = new RedirectToHttpsWwwNonWwwRule
            {
                status_code = 301,
                redirect_to_https = true,
                redirect_to_www = false,
                redirect_to_non_www = true,
                append_slash = true,

            };
            RewriteOptions options = new RewriteOptions();
            options.Rules.Add(rule);
            app.UseRewriter(options);
            app.UseRouting();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {


                IRouteBuilder routeBuilder = routes.MapRoute(template: "{controller=Home}/{action=Index}/{id?}", name: "default");
            });

        }
    }

}
