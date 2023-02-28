using Serilog;
using moviesStorage.Registration;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");
var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console(
        outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(ctx.Configuration));

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();

app.Run();