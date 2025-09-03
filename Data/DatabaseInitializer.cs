using Microsoft.Extensions.Logging;

namespace Listify.Data;
public sealed class DatabaseInitializer : IDatabaseInitializer
{
    private readonly IDatabaseService _db;
    private readonly ILogger<DatabaseInitializer> _logger;
    public DatabaseInitializer(IDatabaseService db, ILogger<DatabaseInitializer> logger)
    { _db = db; _logger = logger; }

    public async Task InitializeAsync()
    {
        _logger.LogInformation("Inicializando base de datos...");
        await _db.EnsureCreatedAsync();
        _logger.LogInformation("Base de datos OK en {Path}", _db.DbPath);
    }
}
