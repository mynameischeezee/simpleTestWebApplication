using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using movieStorage.Identity;
using movieStorage.Identity.Configuration;
using movieStorage.Identity.Data.Identity;
using movieStorage.Identity.ServiceContext;


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateBootstrapLogger();

Log.Information("Starting up");
var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
builder.Services.AddControllers();
builder.Services.AddDbContext<IdentityContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("IdentityConnectionString")));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication();
builder.Services.AddAutoMapper(typeof(MapConfiguration));
builder.Services.AddIdentity<ServiceUser, IdentityRole>().AddEntityFrameworkStores<IdentityContext>();
builder.Services.AddMediatR(typeof(Program));

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console(
        outputTemplate:
        "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}")
    .Enrich.FromLogContext()
    .ReadFrom.Configuration(ctx.Configuration));

var app = builder
    .ConfigureServices()
    .ConfigurePipeline();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();