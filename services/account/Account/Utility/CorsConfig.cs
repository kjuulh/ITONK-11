using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Account.Utility
{
    public class CorsConfig
    {
        public static void AddCorsPolicy(IServiceCollection services)
        {
            services.AddCors(opt =>
            {
                opt.AddPolicy("AllowAll", p => { p.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod(); });
            });
        }

        public static void AddCors(IApplicationBuilder app)
        {
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
        }
    }
}