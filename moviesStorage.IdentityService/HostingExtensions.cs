using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using moviesStorage.IdentityService.Configuration;
using moviesStorage.IdentityService.Data.Identity;
using moviesStorage.IdentityService.Repository;
using moviesStorage.IdentityService.Repository.Abstraction;
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

        builder.Services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = "duende";
            })
            .AddOpenIdConnect("duende", "IdentityServer", options =>
            {
                options.Authority = "https://localhost:5002";
                options.ClientId = "Test";
            
                options.ResponseType = "id_token";
                options.ResponseMode = "query";
                options.SaveTokens = true;

                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("api");
                options.Scope.Add("offline_access");
            });

        builder.Services.AddAuthorization();
        
        builder.Services.AddAutoMapper(typeof(MapConfiguration));
        
        builder.Services.AddIdentity<ServiceUser, IdentityRole>()
            .AddEntityFrameworkStores<IdentityContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddIdentityServer(options => {
                options.ServerSideSessions.UserDisplayNameClaimType = "email";
                options.Authentication.CookieLifetime = TimeSpan.FromMinutes(1);
                options.Authentication.CoordinateClientLifetimesWithUserSession = true;
                
            })
            .AddAspNetIdentity<ServiceUser>()
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiResources(Config.ApiResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddDeveloperSigningCredential()
            .AddServerSideSessions();
        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "My.Identity.Cookie";
            options.LoginPath = "/login";
            options.LogoutPath = "/logout";
            options.ExpireTimeSpan = TimeSpan.FromSeconds(3);
        });
        
        builder.Services.AddMediatR(typeof(Program));

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IGenericRepository<ServiceUser>, GenericRepository<ServiceUser>>();
        
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