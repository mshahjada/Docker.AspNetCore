using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudApp.Core.Interface;
using CloudApp.Core.Service;
using CloudApp.Filters;
using CloudApp.Infra;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CloudApp
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public IWebHostEnvironment _environment { get; }
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public Startup(IWebHostEnvironment environment)
        {
            _environment = environment;
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json");

            _configuration = builder.Build();
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connectionString = _configuration.GetConnectionString("ApplicationDBContext");

            #region Docker Environment Variables
                string server = _configuration["SQL_SERVER"];
                string user = _configuration["SQL_USERNAME"];
                string pass = _configuration["SQL_PASSWORD"];
                string dbName = _configuration["SQL_DATABASE"];
                connectionString = $"Server={server};Database={dbName};User={user};Password={pass};";
            #endregion

            //services.AddEntityFrameworkSqlServer();
            //services.AddEntityFrameworkProxies();
            services.AddDbContextPool<AppDBContext>((serviceProvider, optionBuilder) =>
            {
                optionBuilder.UseSqlServer(connectionString, x =>
                {
                    //x.MigrationsHistoryTable("__EFMigrationsHistory", "dbo");
                    //x.EnableRetryOnFailure(); 
                });
                //optionBuilder.UseLazyLoadingProxies();
                //optionBuilder.UseInternalServiceProvider(serviceProvider);
            });

            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            });


            services.AddControllers();

            //Filter

            services.AddScoped<SampleActionFilter>();
            services.AddControllersWithViews(opt =>
            {
                opt.Filters.Add(typeof(GlobalActionFilter), int.MinValue);
            });

            //services.AddStackExchangeRedisCache(opt => { opt.Configuration = ""; });

            services.AddScoped<IProduct, ProductService>();
            services.AddScoped<IProductCategory, ProductCategoryService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseCors(options =>
            {
                options.AllowAnyOrigin();
                options.AllowAnyHeader();
                options.AllowAnyMethod();
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
