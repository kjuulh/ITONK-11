using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Authentication.Utility {
    public class APIDocumentationInitializer {
        public static void ApiDocumentationInitializer (IServiceCollection services) {
            services.AddSwaggerGen (c => { c.SwaggerDoc ("v1", new Info { Title = "Authentication API", Version = "v1" }); });
        }

        public static void AllowAPIDocumentation (IApplicationBuilder app) {
            app.UseSwagger ();
            app.UseSwaggerUI (c => { c.SwaggerEndpoint ("/swagger/v1/swagger.json", "Authentication API"); });
        }
    }
}