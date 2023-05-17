using graduation.Data.infrastructure.Entities;
using Graduation.WebUI.Infrastructure.Cache;
using Graduation.WebUI.Infrastructure.Rules;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace graduation.WebUI.Site
{

    public class Startup
    {

        public IConfiguration Configuration { get; }
        public IConfigurationRoot ConfigurationRoot { get; }
        [System.Obsolete]
        public Microsoft.AspNetCore.Hosting.IHostingEnvironment Environment { get; }

        [System.Obsolete]
        public Startup(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)

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

        [System.Obsolete]
        public void ConfigureServices(IServiceCollection services)
        {
            if (Environment.IsDevelopment())
            {
                services.AddControllersWithViews().AddRazorRuntimeCompilation();

            }
            services.Configure<DatabaseSettings>(Configuration.GetSection("DatabaseSettings"));
            services.AddOptions();


         


            services.Configure<CookiePolicyOptions>(options =>
                {
                    options.MinimumSameSitePolicy = SameSiteMode.Strict;
                });
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
            app.UseStaticFiles();
            app.UseMvc(routes =>
           {
               routes.MapRoute(name: "category", template: "kategori/{slug}", defaults: new { controller = "Category", action = "Index", page = 1 });
               routes.MapRoute(name: "categoryWithPage", template: "kategori/{slug}/sayfa/{page}", defaults: new { controller = "Category", action = "Index", page = 1 });


               IRouteBuilder routeBuilder = routes.MapRoute(template: "{controller=Home}/{action=Index}/{id?}", name: "default");
           });

        }
    }
}

