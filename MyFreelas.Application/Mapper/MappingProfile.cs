using AutoMapper;
using HashidsNet;
using MyFreelas.Application.Dtos.Customer;
using MyFreelas.Application.Dtos.Freela;
using MyFreelas.Application.Dtos.User;

namespace MyFreelas.Application.Mapper;

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
        CreateMap<RequestRegisterUserJson, Domain.Entities.User>();
        CreateMap<RequestAuthenticationJson, Domain.Entities.User>();
        CreateMap<RequestCustomerJson, Domain.Entities.Customer>();

        CreateMap<RequestRegisterFreelaJson, Domain.Entities.Freela>()
            .ForMember(d => d.CustomerId, cfg => cfg
            .MapFrom(s => _hashids.DecodeSingle(s.CustomerId)));

        CreateMap<RequestUpdateFreelaJson, Domain.Entities.Freela>();

    }

    private void EntityToResponse()
    {
        CreateMap<Domain.Entities.User, ResponseRegisterUserJson>();
        CreateMap<Domain.Entities.User, ResponseAuthenticationJson>();
        CreateMap<Domain.Entities.User, ResponseProfileJson>();

        CreateMap<Domain.Entities.Customer, ResponseRegisterCustomerJson>()
            .ForMember(d => d.Id, cfg => cfg
            .MapFrom(s => _hashids.Encode(s.Id)));

        CreateMap<Domain.Entities.Customer, ResponseCustomerJson>()
            .ForMember(d => d.Id, cfg => cfg
            .MapFrom(s => _hashids.Encode(s.Id)));

        CreateMap<Domain.Entities.Customer, ResponseAllCustomerJson>()
            .ForMember(d => d.Id, cfg => cfg.MapFrom(s => _hashids.Encode(s.Id)))
            .ForMember(d => d.TotalProjects, cfg => cfg.MapFrom(s => s.Freelas.Count));

        CreateMap<Domain.Entities.Freela, ResponseFreelaJson>()
            .ForMember(d => d.Id, cfg => cfg.MapFrom(s => _hashids.Encode(s.Id)));

        CreateMap<Domain.Entities.Freela, ResponseAllFreelasJson>()
            .ForMember(d => d.Id, cfg => cfg
            .MapFrom(s => _hashids.Encode(s.Id)))
            .ForMember(d => d.CustomerId, cfg => cfg
            .MapFrom(s => _hashids.Encode(s.CustomerId)));

        CreateMap<Domain.Entities.Installment, ResponseInstallmentJson>();

        CreateMap<Domain.Entities.Freela, ResponseFreelasByCustomerJson>()
           .ForMember(d => d.Id, cfg => cfg
           .MapFrom(s => _hashids.Encode(s.Id)));

    }

    private void EntityToRequest()
    {
        CreateMap<Domain.Entities.User, RequestAuthenticationJson>();
    }
}
