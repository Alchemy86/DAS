using DAS.DAL2;
using DAS.DAL2.Repositories;
using DAS.Domain;
using DAS.Domain.Users;
using DAS.ServiceCall;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MVC
{
    public class Startup
    {
        private IServiceCalls DasApi;
        private IUserRepository UserRepository;
        private IUnitOfWork UnitOfWork;
        private ISystemRepository SystemRepository;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            UnitOfWork = new Model1();
            SystemRepository = new SystemRepository(UnitOfWork);
            DasApi = new DasService(SystemRepository);
            UserRepository = new UserRepository(UnitOfWork);
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddSession();
            services.AddMvc();
            services.AddSingleton(UnitOfWork);
            services.AddSingleton(SystemRepository);
            services.AddSingleton(UserRepository);
            services.AddSingleton(DasApi);

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseSession();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Index}/{id?}");

                // Add this code to handle non-existing urls
                routes.MapRoute(
                    "404-PageNotFound",
                    "{*url}",
                    new { controller = "Base", action = "Page404" }
                );
            });
        }
    }
}
