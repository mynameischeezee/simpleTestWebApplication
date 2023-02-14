using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using moviesStorage.IdentityService.Configuration;
using moviesStorage.IdentityService.Data.Identity;
using moviesStorage.IdentityService.ServiceContext;
using Serilog;

namespace moviesStorage.IdentityService;

internal static class HostingExtensions
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

        builder.Services.AddAuthorization();
        
        builder.Services.AddAutoMapper(typeof(MapConfiguration));
        
        builder.Services.AddIdentity<ServiceUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddIdentityServer()
            .AddAspNetIdentity<ServiceUser>()
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddDeveloperSigningCredential();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "Identity.Cookie";
            options.LoginPath = "/login";
            options.LogoutPath = "/logout";
        });
        
        builder.Services.AddMediatR(typeof(Program));
        return builder.Build();
    }
    
    public static WebApplication ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.UseIdentityServer();
        app.UseAuthorization();
        app.UseAuthentication();
        app.UseHttpsRedirection();
        app.MapControllers();
        if (!app.Environment.IsDevelopment()) return app;
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseDeveloperExceptionPage();
        return app;
    }
}