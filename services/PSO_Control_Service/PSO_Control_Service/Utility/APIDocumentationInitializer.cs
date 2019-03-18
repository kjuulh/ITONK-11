using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace PSO_Control_Service.Utility
{
    public class APIDocumentationInitializer
    {
        public static void ApiDocumentationInitializer(IServiceCollection services)
        {
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "PSO API", Version = "v1"}); });
        }
        
        
        public static void AllowAPIDocumentation(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "PSO API"); });
        }
    }
}