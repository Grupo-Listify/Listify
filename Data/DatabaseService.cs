using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Maui.Storage;
using SQLite;

namespace Listify.Data;

public sealed class DatabaseService : IDatabaseService, IAsyncDisposable
{
    private readonly ILogger<DatabaseService> _logger;
    private SQLiteAsyncConnection? _conn;
    private bool _ensured;
    public string DbPath { get; }

    public DatabaseService(ILogger<DatabaseService> logger)
    {
        _logger = logger;
        try { SQLitePCL.Batteries_V2.Init(); } catch { }
        var appData = FileSystem.AppDataDirectory;
        DbPath = Path.Combine(appData, "listify.db3");
        _logger.LogInformation("AppDataDirectory: {AppDataDirectory}", appData);
        _logger.LogInformation("DB target path: {DbPath}", DbPath);
    }

    public async Task EnsureCreatedAsync()
    {
        if (_ensured) return;
        var conn = await GetConnectionAsync();
        await conn.CreateTableAsync<HealthCheck>();
        await conn.InsertAsync(new HealthCheck { CreatedAt = DateTime.UtcNow });
        var count = await conn.Table<HealthCheck>().CountAsync();
        _logger.LogInformation("HealthCheck rows: {Count}", count);
        _logger.LogInformation(File.Exists(DbPath) ? "DB ya existía en {DbPath}" : "DB creada en {DbPath}", DbPath);
        _ensured = true;
    }

    public async Task<SQLiteAsyncConnection> GetConnectionAsync()
    {
        if (_conn is not null) return _conn;

        var flags = SQLiteOpenFlags.Create | SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.SharedCache;
        _conn = new SQLiteAsyncConnection(DbPath, flags);

        try
        {
            var mode = await _conn.ExecuteScalarAsync<string>("PRAGMA journal_mode=WAL;");
            _logger.LogInformation("SQLite journal_mode: {Mode}", mode);
        }
        catch (SQLiteException ex)
        {
            _logger.LogWarning(ex, "No se pudo establecer WAL; continuo con el modo por defecto.");
        }

        return _conn;
    }

    public async ValueTask DisposeAsync()
    {
        if (_conn is not null) { try { await _conn.CloseAsync(); } catch { } _conn = null; }
    }
}

public class HealthCheck
{
    [PrimaryKey, AutoIncrement] public int Id { get; set; }
    public DateTime CreatedAt { get; set; }
}
