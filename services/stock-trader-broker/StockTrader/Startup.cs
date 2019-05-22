using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using StockTrader.Database;
using StockTrader.Repositories;
using StockTrader.Services;
using StockTrader.Utility;

namespace StockTrader {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            // Set compability mode for mvc
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2)
                .AddJsonOptions (options => {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });
            APIDocumentationInitializer.ApiDocumentationInitializer (services);
            StartupDatabaseInitializer.InitializeDatabase (services);

            services.AddScoped<IStockTraderRepository, StockTraderRepository> ();
            services.AddScoped<IUnitOfWork, UnitOfWork> ();
            services.AddScoped<ITransactionsRepository, TransactionsRepository> ();

            services.AddScoped<IStockTraderService, StockTraderService> ();
            services.AddScoped<ITransactionsService, TransactionsService> ();

            CorsConfig.AddCorsPolicy (services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IHostingEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            } else {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts ();
                app.UseHttpsRedirection ();
            }

            StartupDatabaseInitializer.MigrateDatabase (app);
            APIDocumentationInitializer.AllowAPIDocumentation (app);
            CorsConfig.AddCors (app);

            app.UseMvc ();
        }
    }
}