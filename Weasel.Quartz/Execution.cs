using Npgsql;
using Weasel.Postgresql;
using Weasel.Postgresql.Tables;
using Weasel.Quartz.Internal;
using Weasel.Quartz.Tables;

namespace Weasel.Quartz;

public static class Quartz
{
    public static Table[] AllTables(string? schema)
    {
        schema ??= PostgresqlProvider.Instance.DefaultDatabaseSchemaName;
        
        return [
            new QrtzBlobTriggersTable(schema), 
            new QrtzCalendarsTable(schema), 
            new QrtzCronTriggersTable(schema),
            new QrtzFiredTriggersTable(schema),
            new QrtzJobDetailsTable(schema),
            new QrtzLocksTable(schema),
            new QrtzSchedulerStateTable(schema),
            new QrtzSimpleTriggersTable(schema),
            new QrtzSimpropTriggersTable(schema),
            new QrtzTriggersTable(schema),
            new QtzPausedTriggerGroupsTable(schema)
        ];
    }
}


public class Execution
{
    public static async Task CreateMigrationAsync(NpgsqlDataSource dataSource, string? schema)
    {
        var db = QuartzDatabase.ForDataSource(dataSource);
        var feature = db.Features["quartz"];
        
        feature.AddTables(Quartz.AllTables(schema));
        
        
    }
}