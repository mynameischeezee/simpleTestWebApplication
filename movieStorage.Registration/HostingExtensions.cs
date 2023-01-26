using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using movieStorage.Registration.Configuration;
using movieStorage.Registration.Data.Identity;
using movieStorage.Registration.ServiceContext;
using Serilog;

namespace movieStorage.Registration;

public static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
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
        return builder.Build();
    }

    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHttpsRedirection();
        app.MapControllers();
        if (!app.Environment.IsDevelopment()) return app;
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseDeveloperExceptionPage();
        return app;
    }
}