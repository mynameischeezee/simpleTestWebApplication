﻿using AutoMapper;
using Duende.IdentityServer.Models;
using moviesStorage.IdentityService.Data.Identity;
using moviesStorage.IdentityService.Models;
using moviesStorage.IdentityService.Responses;

namespace moviesStorage.IdentityService.Configuration;

public class MapConfiguration : Profile
{
    public MapConfiguration()
    {
        this.Configure();
    }

    private void Configure()
    {
        CreateMap<UserDTO, ServiceUser>()
            .ForMember(x => x.Country, opt => opt.MapFrom(dto => dto.Address.Country))
            .ForMember(x => x.PostCode, opt => opt.MapFrom(dto => dto.Address.PostCode))
            .ForMember(x => x.Line1, opt => opt.MapFrom(dto => dto.Address.Line1))
            .ForMember(x => x.Line2, opt => opt.MapFrom(dto => dto.Address.Line2))
            .ForMember(x => x.Town, opt => opt.MapFrom(dto => dto.Address.Town))
            .ForMember(x => x.CountryCode, opt => opt.MapFrom(dto => dto.Address.CountryCode))
            .ReverseMap();

        CreateMap<UserSession, UserSessionResponse>().ReverseMap();
    }
}