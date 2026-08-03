using Weasel.Postgresql.Tables;
using Weasel.Quartz.Postgres.Internal;

namespace Weasel.Quartz.Postgres.Tables;

internal sealed class QrtzJobDetailsTable : QuartzTable
{
    public const string TableName = "qrtz_job_details";
    
    public QrtzJobDetailsTable(string schema) : base(schema, TableName)
    {
        PrimaryKeyName = "qrtz_job_details_pkey";
        
        AddColumn("sched_name", "text").NotNull().AsPrimaryKey();
        AddColumn("job_name", "text").NotNull().AsPrimaryKey();
        AddColumn("job_group", "text").NotNull().AsPrimaryKey();
        AddColumn("description", "text").AllowNulls();
        AddColumn("job_class_name", "text").NotNull();
        AddColumn("is_durable", "bool").NotNull();
        AddColumn("is_nonconcurrent", "bool").NotNull();
        AddColumn("is_update_data", "bool").NotNull();
        AddColumn("requests_recovery", "bool").NotNull();
        AddColumn("job_data", "bytea").AllowNulls();

        Indexes.Add(new IndexDefinition("idx_qrtz_j_g_n")
        {
            Columns = ["sched_name", "job_group", "job_name"]
        });
    }
}