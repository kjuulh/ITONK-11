using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace StockSeller.Utility
{
  public class APIDocumentationInitializer
  {
    public static void ApiDocumentationInitializer(IServiceCollection services)
    {
      services.AddSwaggerGen(c => { c.SwaggerDoc("seller", new Info { Title = "StockSeller API", Version = "v1" }); });
    }

    public static void AllowAPIDocumentation(IApplicationBuilder app)
    {
      app.UseSwagger();
      app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/seller/swagger.json", "StockSeller API"); });
    }
  }
}