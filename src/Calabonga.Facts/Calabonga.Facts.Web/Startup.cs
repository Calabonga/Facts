using Calabonga.AspNetCore.Controllers.Extensions;
using Calabonga.Facts.Web.Data;
using Calabonga.Facts.Web.Infrastructure.Services;
using Calabonga.Facts.Web.Infrastructure.TagHelpers.PagedListTagHelper;
using Calabonga.UnitOfWork;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;

namespace Calabonga.Facts.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration) => Configuration = configuration;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<IdentityOptions>(config =>
            {
                config.Password.RequireDigit = false;
                config.Password.RequireNonAlphanumeric = false;
                config.Password.RequireUppercase = false;
            });

            services.AddRouting(config =>
            {
                config.LowercaseQueryStrings = true;
                config.LowercaseUrls = true;
            });

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();
            services.AddUnitOfWork<ApplicationDbContext>();
            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddAutoMapper(typeof(Startup));
            services.AddCommandAndQueries(typeof(Startup).Assembly);

            services.AddControllersWithViews();

            // dependency injection
            services.AddTransient<IPagerTagHelperService, PagerTagHelperService>();
            services.AddTransient<IFactService, FactService>();

            services.AddServerSideBlazor();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Site/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    "index",
                    "{controller=Facts}/{action=Index}/{tag:regex([a-zА-Я])}/{search:regex([a-zА-Я])}/{pageIndex:int?}");

                endpoints.MapControllerRoute(
                    "index",
                    "{controller=Facts}/{action=Index}/{tag:regex([a-zА-Я])}/{pageIndex:int?}");

                endpoints.MapControllerRoute(
                    "index",
                    "{controller=Facts}/{action=Index}/{pageIndex:int?}");

                endpoints.MapControllerRoute(
                    "default",
                    "{controller=Facts}/{action=Index}/{id?}");

                endpoints.MapRazorPages();

                endpoints.MapBlazorHub();

                #region disable some pages

                // Get
                endpoints.MapGet("/Identity/Account/Register", context => Task.Factory.StartNew(() =>
                    context.Response.Redirect("/Identity/Account/Login?returnUrl=~%2F", true, true)));

                // Post
                endpoints.MapPost("/Identity/Account/Register", context => Task.Factory.StartNew(() => 
                    context.Response.Redirect("/Identity/Account/Login?returnUrl=~%2F", true, true)));
                
                #endregion
            });
        }
    }
}