using Listify.Data;
using Microsoft.Extensions.Logging;

namespace Listify;  

public partial class App : Application
{
    public App(AppShell shell, IDatabaseInitializer dbInit, ILogger<App> logger)
    {
        InitializeComponent();
        MainPage = shell;

        Dispatcher.Dispatch(async () =>
        {
            try
            {
                await dbInit.InitializeAsync();
                logger.LogInformation("DB initialization completed.");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error inicializando la DB");
            }
        });
    }
}
