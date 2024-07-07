using Weasel.Core;
using Weasel.Core.Migrations;
using Weasel.Postgresql.Tables;

namespace Weasel.Quartz.Postgres.Internal;

internal sealed class TableFeature: FeatureSchemaBase
{
    public Dictionary<string, Table> Tables { get; } = new();

    public TableFeature(string identifier, Migrator migrator) : base(identifier, migrator)
    {
    }

    public void AddTables(params Table[] tables)
    {
        foreach (var table in tables)
        {
            Tables.Add(table.Identifier.Name, table);
        }
    }

    protected override IEnumerable<ISchemaObject> schemaObjects()
    {
        return Tables.Values;
    }
}