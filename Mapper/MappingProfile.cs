using AutoMapper;
using myfreelas.Dtos.User;
using myfreelas.Models;

namespace myfreelas.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        RequestForEntity();
        EntityForResponse();
    }

    private void RequestForEntity()
    {
        CreateMap<RequestRegisterUserJson, User>(); 
        CreateMap<RequestAuthenticationJson, User>(); 
    }

    private void EntityForResponse()
    {
        CreateMap<User, ResponseRegisterUserJson>(); 
        CreateMap<User, ResponseAuthenticationJson>();
    }
}
