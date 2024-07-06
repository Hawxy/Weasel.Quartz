using Weasel.Quartz.Internal;

namespace Weasel.Quartz.Tables;

internal class QtzPausedTriggerGroupsTable : QuartzTable
{
    public const string TableName = "qrtz_paused_trigger_grps";
    
    public QtzPausedTriggerGroupsTable(string schema) : base(schema, TableName)
    {
        AddColumn("sched_name", "text").NotNull().AsPrimaryKey();
        AddColumn("trigger_group", "text").NotNull().AsPrimaryKey();
    }
}