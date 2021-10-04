using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using Quartz.Impl;
using Quartz.Spi;
using Schedule;
using Schedule.Jobs;
using Service.Auth;
using Service.Auth.Token;
using Service.ShortenUrl;
using Service.ShortenUrl.Encrypt;
using System.Collections.Generic;
using Web.Filters;
using Web.Vaildations.Auth;

namespace Web.ServiceContainer
{
    public static class DependencyResolver
    {
        public static ServiceProvider Services { get; private set; }
        //public static IEnumerable<JobSchedule> _jobSchedules { get; private set; }
        public static void InitComponents(IServiceCollection services)
        {
            DependencyResolver.InitDbContext(services);
            DependencyResolver.InitService(services);
            DependencyResolver.InitValidator(services);
            //DependencyResolver.InitJobSchedules(services);
            Services = services.BuildServiceProvider();
        }

        private static void InitDbContext(IServiceCollection services)
        {
            //services.AddScoped<DbContext, Model.SaintEir.SaintEirOp>();
            services.AddScoped<DbContext, Model.Sqlite.SQLiteContext>();
        }

        private static void InitService(IServiceCollection services)
        {
            services.AddScoped<IAuthManager, AuthManager<JWT>>();
            services.AddScoped<JWT>();
            services.AddScoped<AuthorizationFilter>();
            services.AddScoped<ExceptionFilter>();
            services.AddScoped<IShortenUrlManager, ShortenUrlManager>();
            services.AddScoped<IEncryptService, EncryptService>();

            //向DI容器註冊Quartz服務
            services.AddSingleton<IJobFactory, JobFactory>();
            services.AddSingleton<ISchedulerFactory, StdSchedulerFactory>();
            //向DI容器註冊Job
            services.AddSingleton<CheckExpiredJob>();
            //向DI容器註冊Host服務
            services.AddSingleton<QuartzHostedService>();
        }

        private static void InitValidator(IServiceCollection services)
        {
            services.AddScoped<IAuthRequestValidator, AuthRequestValidator>();
        }

        //private static void InitJobSchedules(IServiceCollection services)
        //{

        //}
    }
}
