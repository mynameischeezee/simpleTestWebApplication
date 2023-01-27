using AutoMapper;
using movieStorage.Registration.Data.Identity;
using movieStorage.Registration.Models;
using movieStorage.Registration.Responses;

namespace movieStorage.Registration.Configuration;

public class MapConfiguration : Profile
{
    public MapConfiguration()
    {
        this.Configure();
    }

    private void Configure()
    {
        CreateMap<UserDTO, ServiceUser>()
            .ForMember(x=> x.Country, opt => opt.MapFrom(dto => dto.Address.Country))
            .ForMember(x=> x.PostCode, opt => opt.MapFrom(dto => dto.Address.PostCode))
            .ForMember(x=> x.Line1, opt => opt.MapFrom(dto => dto.Address.Line1))
            .ForMember(x=> x.Line2, opt => opt.MapFrom(dto => dto.Address.Line2))
            .ForMember(x=> x.Town, opt => opt.MapFrom(dto => dto.Address.Town))
            .ForMember(x=> x.CountryCode, opt => opt.MapFrom(dto => dto.Address.CountryCode))
            .ForMember(x=> x.Status, opt => opt.MapFrom(dto => dto.Status.ToString()))
            .ReverseMap();

        CreateMap<ServiceUser, RegisterUserResponse>()
            .ForMember(x => x.Id, opt => opt.MapFrom(u => u.Id))
            .ForMember(x => x.Username, opt => opt.MapFrom(u => u.UserName))
            .ForMember(x => x.Email, opt => opt.MapFrom(u => u.Email))
            .ForMember(x => x.Status, opt => opt.MapFrom(u => u.Status));

    }
}