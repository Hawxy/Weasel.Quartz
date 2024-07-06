using Weasel.Postgresql;
using Weasel.Postgresql.Tables;
using Weasel.Quartz.Internal;

namespace Weasel.Quartz.Tables;

internal class QrtzSimpleTriggersTable : QuartzTable
{
    public const string TableName = "qrtz_simple_triggers";
    
    public QrtzSimpleTriggersTable(string schema) : base(schema, TableName)
    {
        AddColumn("sched_name", "text").NotNull().AsPrimaryKey();
        AddColumn("trigger_name", "text").NotNull().AsPrimaryKey();
        AddColumn("trigger_group", "text").NotNull().AsPrimaryKey();
        AddColumn("repeat_count", "bigint").NotNull();
        AddColumn("repeat_interval", "bigint").NotNull();
        AddColumn("times_triggered", "bigint").NotNull();
        
        ForeignKeys.Add(new ForeignKey("qrtz_simple_triggers_sched_name_trigger_name_trigger_group_fkey")
        {
            ColumnNames = ["sched_name", "trigger_name", "trigger_group"],
            LinkedNames = ["sched_name", "trigger_name", "trigger_group"],
            LinkedTable = new PostgresqlObjectName(schema, "qrtz_triggers"),
            OnDelete = CascadeAction.Cascade
        });
    }
}