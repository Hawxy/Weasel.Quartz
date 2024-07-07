using Microsoft.Extensions.Logging;
using Npgsql;
using Weasel.Core.Migrations;
using Weasel.Postgresql;
using Weasel.Postgresql.Tables;
using Weasel.Quartz.Postgres.Internal;
using Weasel.Quartz.Postgres.Tables;

namespace Weasel.Quartz.Postgres;

/// <summary>
/// Entry point for all Quartz schema management capabilities
/// </summary>
public static class QuartzSchema
{
    /// <summary>
    /// Returns all Quartz tables. Useful for plugging into Marten managed schema.
    /// </summary>
    /// <param name="schema">The schema for the applied tables. Defaults to the postgres default "public" if not provided.</param>
    /// <returns>An array of tables.</returns>
    public static Table[] AllTables(string? schema = null)
    {
        schema ??= PostgresqlProvider.Instance.DefaultDatabaseSchemaName;
        
        return [
            new QrtzJobDetailsTable(schema),
            new QrtzTriggersTable(schema),
            new QrtzCalendarsTable(schema), 
            new QrtzCronTriggersTable(schema),
            new QrtzFiredTriggersTable(schema),
            new QrtzLocksTable(schema),
            new QrtzSchedulerStateTable(schema),
            new QrtzBlobTriggersTable(schema), 
            new QrtzSimpleTriggersTable(schema),
            new QrtzSimpropTriggersTable(schema),
            new QtzPausedTriggerGroupsTable(schema)
        ];
    }
    
    /// <summary>
    /// Creates a <see cref="QuartzDatabase"/> that can be used to managed migrations against the target database.
    /// </summary>
    /// <param name="dataSource">A data source for the intended database.</param>
    /// <param name="schema">The schema for the applied tables. Defaults to the postgres default "public" if not provided.</param>
    /// <param name="logger">An optional <see cref="ILogger"/> for managed logging capability.</param>
    /// <returns></returns>
    public static IDatabase<NpgsqlConnection> Create(NpgsqlDataSource dataSource, string? schema = null, ILogger? logger = null)
    {
        var db = QuartzDatabase.ForDataSource(dataSource, logger);
        
        db.Tables.AddTables(AllTables(schema));

        return db;
    }
    
    /// <summary>
    /// Creates a <see cref="QuartzDatabase"/> that can be used to managed migrations against the target database.
    /// </summary>
    /// <param name="connectionString">A connection string for the intended database.</param>
    /// <param name="schema">The schema for the applied tables. Defaults to the postgres default "public" if not provided.</param>
    /// <param name="logger">An optional <see cref="ILogger"/> for managed logging capability.</param>
    /// <returns></returns>
    public static IDatabase<NpgsqlConnection> Create(string connectionString, string? schema = null, ILogger? logger = null)
    {
        var db = QuartzDatabase.ForConnectionString(connectionString, logger);
        
        db.Tables.AddTables(AllTables(schema));

        return db;
    }
}