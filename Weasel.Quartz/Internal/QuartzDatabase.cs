using JasperFx.Core;
using Npgsql;
using Weasel.Core;
using Weasel.Core.Migrations;
using Weasel.Postgresql;

namespace Weasel.Quartz.Internal;

internal class QuartzDatabase : PostgresqlDatabase
{
    public QuartzDatabase(IMigrationLogger logger, AutoCreate autoCreate, Migrator migrator, string identifier, NpgsqlDataSource dataSource) : base(logger, autoCreate, migrator, identifier, dataSource)
    {
    }
    
    public static QuartzDatabase ForDataSource(NpgsqlDataSource dataSource)
    {
        var builder = new NpgsqlConnectionStringBuilder(dataSource.ConnectionString);
        var identifier = builder.Database!;

        return new QuartzDatabase(identifier, dataSource);
    }

    public static QuartzDatabase ForConnectionString(string connectionString)
    {
        var builder = new NpgsqlDataSourceBuilder(connectionString);
        var identifier = builder.ConnectionStringBuilder.Database!;

        return new QuartzDatabase(identifier, builder.Build());
    }

    public QuartzDatabase(string identifier, NpgsqlDataSource dataSource)
        : base(new DefaultMigrationLogger(), AutoCreate.All, new PostgresqlMigrator(), identifier, dataSource)
    {
    }
    
    public LightweightCache<string, TableFeature> Features { get; } = new(name => new TableFeature(name, new PostgresqlMigrator()));

    public override IFeatureSchema[] BuildFeatureSchemas()
    {
        return Features.OfType<IFeatureSchema>().ToArray();
    }
}