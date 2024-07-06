using Weasel.Postgresql;
using Weasel.Postgresql.Tables;
using Weasel.Quartz.Internal;

namespace Weasel.Quartz.Tables;

internal class QrtzSimpropTriggersTable : QuartzTable
{
    public const string TableName = "qrtz_simprop_triggers";
    
    public QrtzSimpropTriggersTable(string schema) : base(schema, TableName)
    {
        AddColumn("sched_name", "text").NotNull().AsPrimaryKey();
        AddColumn("trigger_name", "text").NotNull().AsPrimaryKey();
        AddColumn("trigger_group", "text").NotNull().AsPrimaryKey();
        AddColumn("str_prop_1", "text").AllowNulls();
        AddColumn("str_prop_2", "text").AllowNulls();
        AddColumn("str_prop_3", "text").AllowNulls();
        AddColumn("int_prop_1", "integer").AllowNulls();
        AddColumn("int_prop_2", "integer").AllowNulls();
        AddColumn("long_prop_1", "bigint").AllowNulls();
        AddColumn("long_prop_2", "bigint").AllowNulls();
        AddColumn("dec_prop_1", "numeric").AllowNulls();
        AddColumn("dec_prop_2", "numeric").AllowNulls();
        AddColumn("bool_prop_1", "bool").AllowNulls();
        AddColumn("bool_prop_2", "bool").AllowNulls();
        AddColumn("time_zone_id", "text").AllowNulls();
        
        ForeignKeys.Add(new ForeignKey("qrtz_simprop_triggers_sched_name_trigger_name_trigger_group_fkey")
        {
            ColumnNames = ["sched_name", "trigger_name", "trigger_group"],
            LinkedNames = ["sched_name", "trigger_name", "trigger_group"],
            LinkedTable = new PostgresqlObjectName(schema, "qrtz_triggers"),
            OnDelete = CascadeAction.Cascade
        });
    }
}