using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Web.Filters;
using Web.RouteConfig;
using Web.ServiceContainer;

namespace Web
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

            services.AddCors();

            /*add filters */
            services.AddMvc(configuration =>
            {
                configuration.Filters.Add(new AuthorizationFilter());
                //微軟在3.0之後，開啟Endpoint，MVC不會為AllowAnonymous自動添加AllowAnonymousFilter
                configuration.EnableEndpointRouting = false;
            });
            services.AddAuthorization();


            /*add database */
            //services.AddDbContext<Model.SaintEir.SaintEirOp>(x => x.UseSqlServer("name=ConnectionStrings:SaintEirOp"));
            /*add httpcontext current */
            services.AddHttpContextAccessor();

            /*add service */
            DependencyResolver.InitComponents(services);

            // If using IIS:
            services.Configure<IISServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "CustomPolicy",
                    builder =>
                    {
                        builder.WithOrigins("https://tomz-net5-web.herokuapp.com")
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .AllowCredentials();
                    });
            });
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
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            // Make sure you call this before calling app.UseMvc()
            app.UseCors("CustomPolicy");
            app.UseEndpoints(endpoints =>
            {
                RouteConfiguration.Register(endpoints);
            });
            app.UseMvc();
        }
    }
}
