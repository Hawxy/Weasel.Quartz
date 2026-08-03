using Weasel.Postgresql;
using Weasel.Postgresql.Tables;
using Weasel.Quartz.Postgres.Internal;

namespace Weasel.Quartz.Postgres.Tables;

internal sealed class QrtzTriggersTable : QuartzTable
{
    public const string TableName = "qrtz_triggers";
    
    public QrtzTriggersTable(string schema) : base(schema, TableName)
    {
        PrimaryKeyName = "qrtz_triggers_pkey";
        
        AddColumn("sched_name", "text").NotNull().AsPrimaryKey();
        AddColumn("trigger_name", "text").NotNull().AsPrimaryKey();
        AddColumn("trigger_group", "text").NotNull().AsPrimaryKey();
        AddColumn("job_name", "text").NotNull();
        AddColumn("job_group", "text").NotNull();
        AddColumn("description", "text").AllowNulls();
        AddColumn("next_fire_time", "bigint").AllowNulls();
        AddColumn("prev_fire_time", "bigint").AllowNulls();
        AddColumn("priority", "integer").AllowNulls();
        AddColumn("trigger_state", "text").NotNull();
        AddColumn("trigger_type", "text").NotNull();
        AddColumn("start_time", "bigint").NotNull();
        AddColumn("end_time", "bigint").AllowNulls();
        AddColumn("calendar_name", "text").AllowNulls();
        AddColumn("misfire_instr", "smallint").AllowNulls();
        AddColumn("misfire_orig_fire_time", "bigint").AllowNulls();
        AddColumn("execution_group", "varchar(200)").AllowNulls();
        AddColumn("preferred_node", "varchar(200)").AllowNulls();
        AddColumn("preferred_node_auto", "bool").NotNull().DefaultValueByExpression("FALSE");
        AddColumn("job_data", "bytea").AllowNulls();

        ForeignKeys.Add(new ForeignKey("qrtz_triggers_sched_name_job_name_job_group_fkey")
        {
            ColumnNames = ["sched_name", "job_name", "job_group"],
            LinkedNames = ["sched_name", "job_name", "job_group"],
            LinkedTable = new PostgresqlObjectName(schema, "qrtz_job_details", SchemaUtils.IdentifierUsage.General)
        });

        Indexes.Add(new IndexDefinition("idx_qrtz_t_j")
        {
            Columns = ["sched_name", "job_name", "job_group"]
        });

        Indexes.Add(new IndexDefinition("idx_qrtz_t_c")
        {
            Columns = ["sched_name", "calendar_name"]
        });

        Indexes.Add(new IndexDefinition("idx_qrtz_t_g_n")
        {
            Columns = ["sched_name", "trigger_group", "trigger_name"]
        });

        Indexes.Add(new IndexDefinition("idx_qrtz_t_nft_st")
        {
            Columns = ["sched_name", "trigger_state", "next_fire_time"]
        });
    }
}