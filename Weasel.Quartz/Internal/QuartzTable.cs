using Weasel.Postgresql;
using Weasel.Postgresql.Tables;

namespace Weasel.Quartz.Internal;

internal abstract class QuartzTable : Table
{
    protected QuartzTable(string schema, string tableName) : base(new PostgresqlObjectName(schema, tableName)) {}
}