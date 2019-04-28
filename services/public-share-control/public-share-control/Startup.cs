using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PublicShareControl.Database;
using PublicShareControl.Repositories;
using PublicShareControl.Services;
using PublicShareControl.Utility;
using Swashbuckle.AspNetCore.Swagger;

namespace PublicShareControl
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
      // Set compability mode for mvc
      services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

      CorsConfig.AddCorsPolicy(services);
      APIDocumentationInitializer.ApiDocumentationInitializer(services);
      StartupDatabaseInitializer.InitializeDatabase(services);
      services.AddScoped<IPortfolioRepository, PortfolioRepository>();
      services.AddScoped<IUnitOfWork,UnitOfWork>();
      services.AddScoped<IPortfolioService, PortfolioService>(); 
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
        app.UseHttpsRedirection();
      }

      StartupDatabaseInitializer.MigrateDatabase(app);
      APIDocumentationInitializer.AllowAPIDocumentation(app);
      CorsConfig.AddCors(app);
      app.UseMvc();
    }

  }
}