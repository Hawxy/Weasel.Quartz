using System.Data.Common;
using Microsoft.Extensions.Logging;
using Weasel.Core.Migrations;

namespace Weasel.Quartz.Postgres.Internal;

internal sealed class MicrosoftMigrationLogger : IMigrationLogger
{
    private readonly ILogger _logger;

    public MicrosoftMigrationLogger(ILogger logger)
    {
        _logger = logger;
    }

    public void SchemaChange(string sql)
    {
       _logger.LogSchemaChange(sql);
    }

    public void OnFailure(DbCommand command, Exception ex)
    {
        _logger.LogFailure(ex, command.CommandText);
    }
}

internal static partial class Log
{
    [LoggerMessage(LogLevel.Information, "Executed Quartz schema update SQL {sql}")]
    public static partial void LogSchemaChange(this ILogger logger, string sql);

    [LoggerMessage(LogLevel.Error, "Encountered exception executing Quartz schema update SQL: {sql}")]
    public static partial void LogFailure(this ILogger logger, Exception ex, string sql);
}