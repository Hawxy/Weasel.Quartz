using Microsoft.Extensions.Logging;
using Npgsql;
using Weasel.Core;
using Weasel.Core.Migrations;
using Weasel.Postgresql;

namespace Weasel.Quartz.Postgres.Internal;


internal sealed class QuartzDatabase : PostgresqlDatabase
{
    public QuartzDatabase(IMigrationLogger logger, AutoCreate autoCreate, Migrator migrator, string identifier, NpgsqlDataSource dataSource) : base(logger, autoCreate, migrator, identifier, dataSource)
    {
    }
    
    public static QuartzDatabase ForDataSource(NpgsqlDataSource dataSource, ILogger? logger)
    {
        var builder = new NpgsqlConnectionStringBuilder(dataSource.ConnectionString);
        var identifier = builder.Database!;

        return new QuartzDatabase(identifier, dataSource, logger != null ? new MicrosoftMigrationLogger(logger) : new DefaultMigrationLogger());
    }

    public static QuartzDatabase ForConnectionString(string connectionString, ILogger? logger)
    {
        var builder = new NpgsqlDataSourceBuilder(connectionString);
        var identifier = builder.ConnectionStringBuilder.Database!;

        return new QuartzDatabase(identifier, builder.Build(), logger != null ? new MicrosoftMigrationLogger(logger) : new DefaultMigrationLogger());
    }

    public QuartzDatabase(string identifier, NpgsqlDataSource dataSource, IMigrationLogger logger)
        : base(logger, AutoCreate.CreateOrUpdate, new PostgresqlMigrator(), identifier, dataSource)
    {
    }

    public readonly TableFeature Tables = new("tables", new PostgresqlMigrator());

    public override IFeatureSchema[] BuildFeatureSchemas()
    {
        return [Tables];
    }
}