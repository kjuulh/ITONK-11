using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StockSeller.Services;
using StockSeller.Utility;

namespace StockSeller
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
      APIDocumentationInitializer.ApiDocumentationInitializer(services);

      services.AddHttpClient();
      services.AddScoped<IBankService, BankService>();
      services.AddScoped<IPortfolioService, PortfolioService>();
      services.AddScoped<ITraderService, TraderService>();
      services.AddScoped<ISellerService, SellerService>();

      CorsConfig.AddCorsPolicy(services);
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

      APIDocumentationInitializer.AllowAPIDocumentation(app);
      CorsConfig.AddCors(app);

      app.UseMvc();
    }
  }
}