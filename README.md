# Weasel.Quartz

[![Nuget](https://img.shields.io/nuget/v/Weasel.Quartz.Postgres?label=Weasel.Quartz.Postgres&style=flat-square)](https://www.nuget.org/packages/Weasel.Quartz.Postgres)

This package provides runtime Postgres SQL initialization & migration for Quartz.NET.

Schema updates can be executed standalone or as part of Marten's migration execution.

## Running Standalone Migrations
Ideally this would be executed in a hosted service during startup.

`QuartzSchema.Create(...)` should be called, passing in either a `NpgsqlDataSource` or connection string.

```csharp
// A custom Postgres schema can be optionally provided as a parameter.
var db = QuartzSchema.Create(dbSource);

await db.ApplyAllConfiguredChangesToDatabaseAsync();
```

## Marten

Quartz schema can be added to Marten's own schema management via its configuration:

```csharp
// A custom Postgres schema can be optionally provided as a parameter.
foreach (var table in QuartzSchema.AllTables())
{
    options.Storage.ExtendedSchemaObjects.Add(table); 
}
```

## Future updates

The version of this package will stay in line with Quartz major versions (ie v3 == 3.x.x).

If a new schema version is released, simply updating this package and running the application will bring your DB
up to date.