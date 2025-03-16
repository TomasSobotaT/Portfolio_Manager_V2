using Serilog;
using Serilog.Sinks.MSSqlServer;
using System.Collections.ObjectModel;
using System.Data;

namespace PortfolioManager.BuilderExtensions;

public static class SerilogExtension
{
    public static void AddSerilog(this IHostBuilder host, string connectionString)
    {
        host.UseSerilog((context, config) =>
        {
            config.WriteTo.Logger(lc => lc
                .WriteTo.MSSqlServer(
                    connectionString,
                    sinkOptions: new MSSqlServerSinkOptions { TableName = "SerilogExceptionLogs", AutoCreateSqlTable = true })
                .MinimumLevel.Error()
                .Filter.ByIncludingOnly(logEvent => logEvent.Level == Serilog.Events.LogEventLevel.Error));

            config.WriteTo.Logger(lc => lc
                .WriteTo.MSSqlServer(
                    connectionString,
                    sinkOptions: new MSSqlServerSinkOptions { TableName = "SerilogRequestLogs", AutoCreateSqlTable = true },
                    columnOptions: new ColumnOptions
                    {
                        AdditionalColumns = new Collection<SqlColumn>
                        {
                            new SqlColumn { ColumnName = "UserId", DataType = SqlDbType.NVarChar, AllowNull = true, DataLength = 50 }
                        }
                    })
                .MinimumLevel.Information()
                    .Filter.ByIncludingOnly(logEvent =>
                        logEvent.Properties.TryGetValue("SourceContext", out var sourceContext) && 
                        sourceContext.ToString().Contains("LoggingMiddleware"))
                    .Filter.ByExcluding(logEvent =>
                        logEvent.Properties.TryGetValue("Path", out var requestPath) &&
                        (
                            requestPath.ToString().Contains("/swagger", StringComparison.OrdinalIgnoreCase) ||
                            requestPath.ToString().Contains("/_framework", StringComparison.OrdinalIgnoreCase) ||
                            requestPath.ToString().Contains("/browserLink", StringComparison.OrdinalIgnoreCase) ||
                            requestPath.ToString().Contains("/index.html", StringComparison.OrdinalIgnoreCase)
                        )));

            config.WriteTo.Logger(lc => lc
                .WriteTo.MSSqlServer(
                    connectionString,
                    sinkOptions: new MSSqlServerSinkOptions { TableName = "SerilogCustomLogs", AutoCreateSqlTable = true })
                .MinimumLevel.Verbose()
                .Filter.ByIncludingOnly(logEvent => logEvent.Properties.ContainsKey("CustomLog")));
        });
    }

}
