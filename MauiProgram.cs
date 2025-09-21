using Microsoft.Extensions.Logging;
using Listify.Servicios;
using Listify.ViewModels;

namespace Listify;

public static class MauiProgram
{
    /// <summary>
    /// Crea y configura la aplicacion MAUI.
    /// Agrega fuentes, habilita logging en modo debug y registra servicios para DI.
    /// </summary>
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();

        // App principal y fuentes de la UI
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                // Las fuentes deben existir en Resources/Fonts
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

#if DEBUG
        // Salida de logs de depuracion en la consola del IDE
        builder.Logging.AddDebug();
#endif

        // -------------------------------------------------
        // Registro de servicios (Inyeccion de dependencias)
        // -------------------------------------------------

        // Servicio de base de datos: una sola instancia para toda la app
        builder.Services.AddSingleton<BaseDeDatosServicio>();

        // ViewModels:
        // - Principal: singleton para conservar estado entre paginas
        builder.Services.AddSingleton<PrincipalVistaModelo>();
        // - Edicion: transient para obtener una instancia nueva por navegacion
        builder.Services.AddTransient<EditarArticuloVistaModelo>();

        // Construye la app y expone el proveedor de servicios
        var app = builder.Build();

        // Hace accesible el ServiceProvider para obtener instancias en paginas
        ServicioLocalizador.Servicios = app.Services;

        return app;
    }
}
