namespace moviesStorage.Registration.Tests.AuthenticationTests.Mocks;

public class FakeAutoMapper
{
    private IMapper _mapper;
    public FakeAutoMapper()
    {
        _mapper = new Mock<IMapper>().Object;
    }

    public IMapper Create()
    {
        return _mapper;
    }
}