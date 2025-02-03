using syfora_test_DB.Utils;

namespace syfora_test_WebServer
{
    public static class ApplicationBuilder
    {
        public static WebApplicationBuilder InitializeServices(this WebApplicationBuilder builder)
        {
            builder.WebHost.ConfigureKestrel(options =>
            {
                options.ListenAnyIP(5000);
            });

            builder.Logging.ClearProviders();
            builder.Logging.AddConsole();

            builder.Services.AddControllers();

            builder.Services.AddScoped<IUserUtils, UserUtils>();


            return builder;
        }
    }
}
