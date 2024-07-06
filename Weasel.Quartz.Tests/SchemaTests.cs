using Npgsql;
using Weasel.Core;
using Weasel.Quartz.Postgres;

namespace Weasel.Quartz.Tests;

public class SchemaTests
{
    public const string ConnectionString = "host=localhost;database=quartz;password=Password12!;username=postgres";
    [Fact]
    public async Task WeaselQuartzSchema_MatchesQuartzSql()
    {
        await using var dbSource = NpgsqlDataSource.Create(ConnectionString);

        var sql = await File.ReadAllTextAsync("quartz_postgressql.sql");

        await using var command = dbSource.CreateCommand(sql);

        await command.ExecuteNonQueryAsync();
        
        var db = QuartzSchema.Create(dbSource);

        await db.AssertDatabaseMatchesConfigurationAsync();
    }

    [Fact]
    public async Task WeaselSchema_CanBeAppliedToCustomSchema()
    {
        await using var dbSource = NpgsqlDataSource.Create(ConnectionString);
        
        var db = QuartzSchema.Create(dbSource, "quartz");

        await db.ApplyAllConfiguredChangesToDatabaseAsync(AutoCreate.CreateOnly);
    }
}