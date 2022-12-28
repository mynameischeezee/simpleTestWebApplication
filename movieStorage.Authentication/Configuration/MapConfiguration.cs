using AutoMapper;
using movieStorage.Authentication.Data.Identity;
using movieStorage.Authentication.Models;

namespace movieStorage.Authentication.Configuration;

public class MapConfiguration : Profile
{
    public MapConfiguration()
    {
        CreateMap<ServiceUser, UserDTO>().ReverseMap();
    }
}