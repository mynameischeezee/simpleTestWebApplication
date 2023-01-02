using AutoMapper;
using movieStorage.Identity.Data.Identity;
using movieStorage.Identity.Models;

namespace movieStorage.Identity.Configuration;

public class MapConfiguration : Profile
{
    public MapConfiguration()
    {
        this.Configure();
    }

    private void Configure()
    {
        CreateMap<ServiceUser, UserDTO>().ReverseMap();
    }
}