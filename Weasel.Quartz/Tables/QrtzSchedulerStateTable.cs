using Weasel.Quartz.Internal;

namespace Weasel.Quartz.Tables;

internal class QrtzSchedulerStateTable : QuartzTable
{
    public const string TableName = "qrtz_scheduler_state";
    
    public QrtzSchedulerStateTable(string schema) : base(schema, TableName)
    {
        AddColumn("sched_name", "text").NotNull().AsPrimaryKey();
        AddColumn("instance_name", "text").NotNull().AsPrimaryKey();
        AddColumn("last_checkin_time", "bigint").NotNull();
        AddColumn("checkin_interval", "bigint").NotNull();
    }
}