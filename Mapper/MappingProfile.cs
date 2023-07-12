using AutoMapper;
using myfreelas.Dtos.Customer;
using myfreelas.Dtos.User;
using myfreelas.Models;

namespace myfreelas.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        RequestToEntity();
        EntityToResponse();
        EntityToRequest();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRegisterUserJson, User>(); 
        CreateMap<RequestAuthenticationJson, User>();
        CreateMap<RequestRegisterCustomerJson, Customer>(); 
    }

    private void EntityToResponse()
    {
        CreateMap<User, ResponseRegisterUserJson>(); 
        CreateMap<User, ResponseAuthenticationJson>();
        CreateMap<User, ResponseProfileJson>();
        CreateMap<Customer, ResponseRegisterCustomerJson>();
        CreateMap<Customer, ResponseCustomerJson>();
    }

    private void EntityToRequest()
    {
        CreateMap<User, RequestAuthenticationJson>();
    }
}
