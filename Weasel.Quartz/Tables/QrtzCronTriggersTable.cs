using Weasel.Postgresql;
using Weasel.Postgresql.Tables;
using Weasel.Quartz.Internal;

namespace Weasel.Quartz.Tables;

internal class QrtzCronTriggersTable : QuartzTable
{
    public const string TableName = "qrtz_cron_triggers";
    
    public QrtzCronTriggersTable(string schema) : base(schema, TableName)
    {
        AddColumn("sched_name", "text").NotNull().AsPrimaryKey();
        AddColumn("trigger_name", "text").NotNull().AsPrimaryKey();
        AddColumn("trigger_group", "text").NotNull().AsPrimaryKey();
        AddColumn("cron_expression", "text").NotNull();
        AddColumn("time_zone_id", "text").AllowNulls();
        
        ForeignKeys.Add(new ForeignKey("qrtz_cron_triggers_sched_name_trigger_name_trigger_group_fkey")
        {
            ColumnNames = ["sched_name", "trigger_name", "trigger_group"],
            LinkedNames = ["sched_name", "trigger_name", "trigger_group"],
            LinkedTable = new PostgresqlObjectName(schema, "qrtz_triggers"),
            OnDelete = CascadeAction.Cascade
        });
    }
}