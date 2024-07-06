using Weasel.Postgresql;
using Weasel.Postgresql.Tables;
using Weasel.Quartz.Internal;

namespace Weasel.Quartz.Tables;

internal class QrtzTriggersTable : QuartzTable
{
    public const string TableName = "qrtz_triggers";
    
    public QrtzTriggersTable(string schema) : base(schema, TableName)
    {
        AddColumn("sched_name", "text").NotNull().AsPrimaryKey();
        AddColumn("trigger_name", "text").NotNull().AsPrimaryKey();
        AddColumn("trigger_group", "text").NotNull().AsPrimaryKey();
        AddColumn("job_name", "text").NotNull();
        AddColumn("job_group", "text").NotNull();
        AddColumn("description", "text").AllowNulls();
        AddColumn("next_fire_time", "bigint").AllowNulls().AddIndex(c=> c.Name = "idx_qrtz_t_next_fire_time");
        AddColumn("prev_fire_time", "bigint").AllowNulls();
        AddColumn("priority", "integer").AllowNulls();
        AddColumn("trigger_state", "text").NotNull().AddIndex(c => c.Name = "idx_qrtz_t_state");
        AddColumn("trigger_type", "text").NotNull();
        AddColumn("start_time", "bigint").NotNull();
        AddColumn("end_time", "bigint").AllowNulls();
        AddColumn("calendar_name", "text").AllowNulls();
        AddColumn("misfire_instr", "smallint").AllowNulls();
        AddColumn("job_data", "bytea").AllowNulls();
        
        ForeignKeys.Add(new ForeignKey("qrtz_triggers_sched_name_job_name_job_group_fkey")
        {
            ColumnNames = ["sched_name", "job_name", "job_group"],
            LinkedNames = ["sched_name", "job_name", "job_group"],
            LinkedTable = new PostgresqlObjectName(schema, "qrtz_job_details")
        });
        
        Indexes.Add(new IndexDefinition("idx_qrtz_t_nft_st")
        {
            Columns = ["next_fire_time", "trigger_state"]
        });
    }
}