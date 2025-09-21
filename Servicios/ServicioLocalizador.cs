using Microsoft.Extensions.DependencyInjection;

namespace Listify.Servicios
{
    // Esta clase es como un "atajo" para poder acceder a los servicios
    // que registramos en la app (por ejemplo la base de datos).
    // En lugar de estar pasando dependencias por todos lados,
    // usamos este localizador para obtenerlas cuando se necesiten.
    public static class ServicioLocalizador
    {
        // Aquí se guarda el contenedor de servicios que configuramos en MauiProgram.cs
        public static IServiceProvider Servicios { get; set; } = default!;

        // Con este método pedimos un servicio ya registrado.
        // Ejemplo: var db = ServicioLocalizador.Obtener<BaseDeDatosServicio>();
        public static T Obtener<T>() where T : notnull
            => Servicios.GetRequiredService<T>();
    }
}
