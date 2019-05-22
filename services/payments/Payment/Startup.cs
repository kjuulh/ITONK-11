using System.Text;
using Payment.Database;
using Payment.Providers;
using Payment.Repositories;
using Payment.Services;
using Payment.Utility;
using Microsoft.AspNetCore.Payment.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Payment {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {
            // Set compability mode for mvc
            services.AddMvc ().SetCompatibilityVersion (CompatibilityVersion.Version_2_2);
            APIDocumentationInitializer.ApiDocumentationInitializer (services);
            StartupDatabaseInitializer.InitializeDatabase (services);

            services.AddHttpClient();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<IPaymentRepository, PaymentRepository> ();
            services.AddScoped<IUnitOfWork, UnitOfWork> ();
            services.AddScoped<IPaymentService, PaymentService> ();

            CorsConfig.AddCorsPolicy (services);

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            services.AddPayment(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.PaymentScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.PaymentScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
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

            app.UsePayment();
            app.UseMvc ();
        }
    }
}