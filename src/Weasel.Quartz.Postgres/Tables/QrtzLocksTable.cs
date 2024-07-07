using Weasel.Quartz.Postgres.Internal;

namespace Weasel.Quartz.Postgres.Tables;

internal sealed class QrtzLocksTable : QuartzTable
{
    public const string TableName = "qrtz_locks";
    
    public QrtzLocksTable(string schema) : base(schema, TableName)
    {
        PrimaryKeyName = "qrtz_locks_pkey";
        
        AddColumn("sched_name", "text").NotNull().AsPrimaryKey();
        AddColumn("lock_name", "text").NotNull().AsPrimaryKey();
    }
}