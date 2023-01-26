using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace movieStorage.Registration.Tests.AuthenticationTests.Mocks;

public class FakeSignInManager<T> : SignInManager<T> where T : class 
{
    public FakeSignInManager(UserManager<T> userManager) : base(userManager,
        new Mock<IHttpContextAccessor>().Object,
        new Mock<IUserClaimsPrincipalFactory<T>>().Object,
        new Mock<IOptions<IdentityOptions>>().Object,
        new Mock<ILogger<SignInManager<T>>>().Object,
        new Mock<IAuthenticationSchemeProvider>().Object,
        new Mock<IUserConfirmation<T>>().Object)
    {
        
    }        
}