using Weasel.Quartz.Postgres.Internal;

namespace Weasel.Quartz.Postgres.Tables;

internal sealed class QtzPausedTriggerGroupsTable : QuartzTable
{
    public const string TableName = "qrtz_paused_trigger_grps";
    
    public QtzPausedTriggerGroupsTable(string schema) : base(schema, TableName)
    {
        PrimaryKeyName = "qrtz_paused_trigger_grps_pkey";
        
        AddColumn("sched_name", "text").NotNull().AsPrimaryKey();
        AddColumn("trigger_group", "text").NotNull().AsPrimaryKey();
    }
}