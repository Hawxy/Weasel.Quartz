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
    /// 
    /// </summary>
    /// <param name="schema"></param>
    /// <returns></returns>
    public static Table[] AllTables(string? schema)
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
    /// 
    /// </summary>
    /// <param name="dataSource"></param>
    /// <param name="schema"></param>
    /// <param name="logger"></param>
    /// <returns></returns>
    public static IDatabase<NpgsqlConnection> Create(NpgsqlDataSource dataSource, string? schema = null, ILogger? logger = null)
    {
        var db = QuartzDatabase.ForDataSource(dataSource, logger);
        
        db.Tables.AddTables(AllTables(schema));

        return db;
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="connectionString"></param>
    /// <param name="schema"></param>
    /// <param name="logger"></param>
    /// <returns></returns>
    public static IDatabase<NpgsqlConnection> Create(string connectionString, string? schema = null, ILogger? logger = null)
    {
        var db = QuartzDatabase.ForConnectionString(connectionString, logger);
        
        db.Tables.AddTables(AllTables(schema));

        return db;
    }
}