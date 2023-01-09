using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace movieStorage.Identity.Tests.AuthenticationTests.Mocks;

public class FakeUserManager<T>  where T : class
{
    private readonly UserManager<T> _userManager;
    public FakeUserManager()
    {
        var store = new Mock<IUserStore<T>>();
        var mgr = new Mock<UserManager<T>>(store.Object, null, null, null, null, null, null, null, null);
        mgr.Object.UserValidators.Add(new UserValidator<T>());
        mgr.Object.PasswordValidators.Add(new PasswordValidator<T>());

        mgr.Setup(x => x.DeleteAsync(It.IsAny<T>())).ReturnsAsync(IdentityResult.Success);
        mgr.Setup(x => x.CreateAsync(It.IsAny<T>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
        mgr.Setup(x => x.CreateAsync(It.IsAny<T>(), null)).ReturnsAsync(IdentityResult.Failed());
        mgr.Setup(x => x.CreateAsync(null, null)).ReturnsAsync(IdentityResult.Failed());
        mgr.Setup(x => x.UpdateAsync(It.IsAny<T>())).ReturnsAsync(IdentityResult.Success);

        _userManager = mgr.Object;
    }

    public UserManager<T> Create()
    {
        return _userManager;
    }
}