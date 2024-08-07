﻿using Weasel.Postgresql;
using Weasel.Postgresql.Tables;
using Weasel.Quartz.Postgres.Internal;

namespace Weasel.Quartz.Postgres.Tables;

internal sealed class QrtzBlobTriggersTable : QuartzTable
{
    public const string TableName = "qrtz_blob_triggers";
    
    public QrtzBlobTriggersTable(string schema) : base(schema, TableName)
    {
        PrimaryKeyName = "qrtz_blob_triggers_pkey";
        
        AddColumn("sched_name", "text").NotNull().AsPrimaryKey();
        AddColumn("trigger_name", "text").NotNull().AsPrimaryKey();
        AddColumn("trigger_group", "text").NotNull().AsPrimaryKey();
        AddColumn("blob_data", "bytea").AllowNulls();
        
        ForeignKeys.Add(new ForeignKey("qrtz_blob_triggers_sched_name_trigger_name_trigger_group_fkey")
        {
            ColumnNames = ["sched_name", "trigger_name", "trigger_group"],
            LinkedNames = ["sched_name", "trigger_name", "trigger_group"],
            LinkedTable = new PostgresqlObjectName(schema, "qrtz_triggers"),
            OnDelete = CascadeAction.Cascade
        });
    }
}