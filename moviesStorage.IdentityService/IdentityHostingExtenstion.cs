namespace moviesStorage.IdentityService;

public static class IdentityHostingExtenstion
{
    public static WebApplicationBuilder ConfigureIdentityServer(this WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        builder.Services.AddIdentityServer();
        return builder;
    }

    public static WebApplication ConfigureIdentityServerPipeline(this WebApplication app)
    {
        app.UseIdentityServer();
        return app;
    }
}