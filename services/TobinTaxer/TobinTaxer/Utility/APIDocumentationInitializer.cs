using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace TobinTaxer.Utility
{
    public class APIDocumentationInitializer
    {
        public static void ApiDocumentationInitializer(IServiceCollection services)
        {
            services.AddSwaggerGen(c => { c.SwaggerDoc("taxer", new Info {Title = "TobinTaxer API", Version = "v1"}); });
        }
        
        
        public static void AllowAPIDocumentation(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/taxer/swagger.json", "TobinTaxer API"); });
        }
    }
}