using Weasel.Quartz.Internal;

namespace Weasel.Quartz.Tables;

internal class QrtzCalendarsTable : QuartzTable
{
    public const string TableName = "qrtz_calendars";
    
    public QrtzCalendarsTable(string schema) : base(schema, TableName)
    {
        AddColumn("sched_name", "text").NotNull().AsPrimaryKey();
        AddColumn("calendar_name", "text").NotNull().AsPrimaryKey();
        AddColumn("calendar", "bytea").NotNull();
    }
}