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
        AddColumn("trigger_name", "text").NotNull().AddIndex(c=> c.Name = "idx_qrtz_ft_trig_name");
        AddColumn("trigger_group", "text").NotNull().AddIndex(c=> c.Name = "idx_qrtz_ft_trig_group");
        AddColumn("instance_name", "text").NotNull().AddIndex(c => c.Name = "idx_qrtz_ft_trig_inst_name");
        AddColumn("fired_time", "bigint").NotNull();
        AddColumn("sched_time", "bigint").NotNull();
        AddColumn("priority", "integer").NotNull();
        AddColumn("state", "text").NotNull();
        AddColumn("job_name", "text").AllowNulls().AddIndex(c=> c.Name = "idx_qrtz_ft_job_name");
        AddColumn("job_group", "text").AllowNulls().AddIndex(c=> c.Name = "idx_qrtz_ft_job_group");
        AddColumn("is_nonconcurrent", "bool").NotNull();
        AddColumn("requests_recovery", "bool").AllowNulls().AddIndex(c=> c.Name = "idx_qrtz_ft_job_req_recovery");
        
        Indexes.Add(new IndexDefinition("idx_qrtz_ft_trig_nm_gp")
        {
            Columns = ["sched_name", "trigger_name", "trigger_group"]
        });
    }
}