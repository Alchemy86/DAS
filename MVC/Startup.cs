using DAS.DAL2;
using DAS.DAL2.Repositories;
using DAS.Domain;
using DAS.Domain.GoDaddy;
using DAS.Domain.Users;
using DAS.ServiceCall;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace MVC
{
    public class Startup
    {
        private readonly IServiceCalls dasApi;
        private readonly IUserRepository userRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly ISystemRepository systemRepository;
        private readonly IEmail emailService;
        private readonly IAuctionRepository auctionRepository;

        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            unitOfWork = new Model1();
            systemRepository = new SystemRepository(unitOfWork);
            dasApi = new DasService(systemRepository);
            userRepository = new UserRepository(unitOfWork);
            Configuration = builder.Build();
            emailService = new Email(systemRepository);
            auctionRepository = new AuctionRepository(unitOfWork);
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddSession();
            services.AddMvc().AddRazorOptions(options =>
            {
                //Correct issue with ref object outside the current namespace - VS bug.
                var previous = options.CompilationCallback;
                options.CompilationCallback = context =>
                {
                    previous?.Invoke(context);
                    context.Compilation = context.Compilation.AddReferences(MetadataReference.CreateFromFile(typeof(Auction).Assembly.Location));
                };
            });
            services.AddSingleton(unitOfWork);
            services.AddSingleton(systemRepository);
            services.AddSingleton(userRepository);
            services.AddSingleton(dasApi);
            services.AddSingleton(emailService);
            services.AddSingleton(auctionRepository);
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

                routes.MapRoute(
                    name: "defaul2t",
                    template: "{controller=Login}");

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
