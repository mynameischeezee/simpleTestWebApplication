using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace movieStorage.Identity.Tests.AuthenticationTests.Mocks;

public class FakeLogger
{
    private readonly ILogger<AuthenticationController> _logger;
    public FakeLogger()
    {
        var serviceProvider = new ServiceCollection()
            .AddLogging()
            .BuildServiceProvider();
        var factory = serviceProvider.GetService<ILoggerFactory>();
        _logger = factory.CreateLogger<AuthenticationController>();
    }

    public ILogger<AuthenticationController> Create()
    {
        return _logger;
    }
}