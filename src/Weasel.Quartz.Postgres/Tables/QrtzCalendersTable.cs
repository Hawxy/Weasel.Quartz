using Weasel.Quartz.Postgres.Internal;

namespace Weasel.Quartz.Postgres.Tables;

internal sealed class QrtzCalendarsTable : QuartzTable
{
    public const string TableName = "qrtz_calendars";
    
    public QrtzCalendarsTable(string schema) : base(schema, TableName)
    {
        PrimaryKeyName = "qrtz_calendars_pkey";
        
        AddColumn("sched_name", "text").NotNull().AsPrimaryKey();
        AddColumn("calendar_name", "text").NotNull().AsPrimaryKey();
        AddColumn("calendar", "bytea").NotNull();
    }
}