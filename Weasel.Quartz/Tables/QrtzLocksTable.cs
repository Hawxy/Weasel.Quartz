using Weasel.Quartz.Internal;

namespace Weasel.Quartz.Tables;

internal class QrtzLocksTable : QuartzTable
{
    public const string TableName = "qrtz_locks";
    
    public QrtzLocksTable(string schema) : base(schema, TableName)
    {
        AddColumn("sched_name", "text").NotNull().AsPrimaryKey();
        AddColumn("lock_name", "text").NotNull().AsPrimaryKey();
    }
}