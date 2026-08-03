using Weasel.Postgresql.Tables;
using Weasel.Quartz.Postgres.Internal;

namespace Weasel.Quartz.Postgres.Tables;

internal sealed class QrtzFiredTriggersTable : QuartzTable
{
    public const string TableName = "qrtz_fired_triggers";
    
    public QrtzFiredTriggersTable(string schema) : base(schema, TableName)
    {
        PrimaryKeyName = "qrtz_fired_triggers_pkey";
        
        AddColumn("sched_name", "text").NotNull().AsPrimaryKey();
        AddColumn("entry_id", "text").NotNull().AsPrimaryKey();
        AddColumn("trigger_name", "text").NotNull();
        AddColumn("trigger_group", "text").NotNull();
        AddColumn("instance_name", "text").NotNull();
        AddColumn("fired_time", "bigint").NotNull();
        AddColumn("sched_time", "bigint").NotNull();
        AddColumn("priority", "integer").NotNull();
        AddColumn("state", "text").NotNull();
        AddColumn("job_name", "text").AllowNulls();
        AddColumn("job_group", "text").AllowNulls();
        AddColumn("is_nonconcurrent", "bool").NotNull();
        AddColumn("requests_recovery", "bool").AllowNulls();
        AddColumn("execution_group", "varchar(200)").AllowNulls();

        Indexes.Add(new IndexDefinition("idx_qrtz_ft_inst_job_req_rcvry")
        {
            Columns = ["sched_name", "instance_name", "requests_recovery"]
        });

        Indexes.Add(new IndexDefinition("idx_qrtz_ft_j_g")
        {
            Columns = ["sched_name", "job_name", "job_group"]
        });

        Indexes.Add(new IndexDefinition("idx_qrtz_ft_t_g")
        {
            Columns = ["sched_name", "trigger_name", "trigger_group"]
        });
    }
}