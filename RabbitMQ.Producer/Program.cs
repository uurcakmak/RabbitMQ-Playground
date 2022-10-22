using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.CookiePolicy;
using RabbitMQ.Helper.Abstraction;

namespace RabbitMQ.Producer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews()
                .AddRazorRuntimeCompilation()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.DictionaryKeyPolicy = null;
                    options.JsonSerializerOptions.DefaultIgnoreCondition =
                        System.Text.Json.Serialization.JsonIgnoreCondition.Never;
                });

            AddAppDependencies(builder.Services, builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            
            app.UseAuthorization();

            var antiForgery = app.Services.GetRequiredService<IAntiforgery>();

            app.Use((context, next) =>
            {
                var requestPath = context.Request.Path.Value;

                if (string.Equals(requestPath, "/", StringComparison.OrdinalIgnoreCase) || string.Equals(requestPath, "/index.html", StringComparison.OrdinalIgnoreCase))
                {
                    var tokenSet = antiForgery.GetAndStoreTokens(context);
                    context.Response.Cookies.Append("RequestVerificationToken", tokenSet.RequestToken!, new CookieOptions { HttpOnly = false });
                }

                return next(context);
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }

        private static void AddAppDependencies(IServiceCollection services, ConfigurationManager config)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddCookiePolicy(options =>
            {
                options.HttpOnly = HttpOnlyPolicy.None;
                options.Secure = CookieSecurePolicy.Always;
            });

            services.AddAntiforgery(options =>
            {
                options.FormFieldName = "__RequestVerificationToken";
                options.HeaderName = "RequestVerificationToken";
                options.SuppressXFrameOptionsHeader = false;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                options.Cookie.SameSite = SameSiteMode.Strict;
            });

            services.AddSingleton<IProducer>(_ => new Helper.Implementation.Producer(config["RabbitMQ:ConnectionString"]));
            services.AddSingleton<IRabbitApi>(_ => new Helper.Implementation.RabbitApi(config["RabbitMQ:ApiUrl"], config["RabbitMQ:Username"], config["RabbitMQ:Password"]));
        }
    }
}