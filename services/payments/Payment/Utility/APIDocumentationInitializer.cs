using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Payment.Utility
{
    public class APIDocumentationInitializer
    {
        public static void ApiDocumentationInitializer(IServiceCollection services)
        {
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new Info {Title = "Payment API", Version = "v1"}); });
        }

        public static void AllowAPIDocumentation(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Payment API"); });
        }
    }
}