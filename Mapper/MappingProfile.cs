using AutoMapper;
using HashidsNet;
using myfreelas.Dtos;
using myfreelas.Dtos.Customer;
using myfreelas.Dtos.Dashboard;
using myfreelas.Dtos.Freela;
using myfreelas.Dtos.User;
using myfreelas.Models;

namespace myfreelas.Mapper;

public class MappingProfile : Profile
{
    private readonly IHashids _hashids;
    public MappingProfile(IHashids hashids)
    {
        _hashids = hashids;
        RequestToEntity();
        EntityToResponse();
        EntityToRequest();
    }

    private void RequestToEntity()
    {
        CreateMap<RequestRegisterUserJson, User>();
        CreateMap<RequestAuthenticationJson, User>();
        CreateMap<RequestCustomerJson, Customer>();

        CreateMap<RequestRegisterFreelaJson, Freela>()
            .ForMember(d => d.CustomerId, cfg => cfg
            .MapFrom(s => _hashids.DecodeSingle(s.CustomerId)));

        CreateMap<RequestUpdateFreelaJson, Freela>();

        CreateMap<RequestRegisterContractJson, Contract>();

    }

    private void EntityToResponse()
    {
        CreateMap<User, ResponseRegisterUserJson>();
        CreateMap<User, ResponseAuthenticationJson>();
        CreateMap<User, ResponseProfileJson>();

        CreateMap<Customer, ResponseRegisterCustomerJson>()
            .ForMember(d => d.Id, cfg => cfg
            .MapFrom(s => _hashids.Encode(s.Id)));

        CreateMap<Customer, ResponseCustomerJson>()
            .ForMember(d => d.Id, cfg => cfg
            .MapFrom(s => _hashids.Encode(s.Id)));

        CreateMap<Customer, ResponseAllCustomerJson>()
            .ForMember(d => d.Id, cfg => cfg.MapFrom(s => _hashids.Encode(s.Id)))
            .ForMember(d => d.TotalProjects, cfg => cfg.MapFrom(s => s.Freelas.Count));

        CreateMap<Freela, ResponseFreelaJson>()
            .ForMember(d => d.Id, cfg => cfg.MapFrom(s => _hashids.Encode(s.Id)));

        CreateMap<Contract, ResponseContractJson>()
            .ForMember(d => d.Id, cfg => cfg.MapFrom(s => _hashids.Encode(s.Id)));

        CreateMap<Freela, ResponseAllFreelasJson>()
            .ForMember(d => d.Id, cfg => cfg
            .MapFrom(s => _hashids.Encode(s.Id)))
            .ForMember(d => d.CustomerId, cfg => cfg
            .MapFrom(s => _hashids.Encode(s.CustomerId)));


    }

    private void EntityToRequest()
    {
        CreateMap<User, RequestAuthenticationJson>();
    }
}
