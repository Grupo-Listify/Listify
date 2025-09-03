using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks;
using SQLite;


namespace Listify.Data;

public interface IDatabaseService
{
    string DbPath { get; }
    Task<SQLite.SQLiteAsyncConnection> GetConnectionAsync();
    Task EnsureCreatedAsync();
}