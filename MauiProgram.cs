using Microsoft.Extensions.Logging;
using Listify.Data;

namespace Listify
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            
            builder.Services.AddSingleton<IDatabaseService, DatabaseService>();
            builder.Services.AddSingleton<IDatabaseInitializer, DatabaseInitializer>();
            builder.Services.AddSingleton<AppShell>();   
            builder.Services.AddSingleton<MainPage>();   
            return builder.Build();
        }
    }
}
