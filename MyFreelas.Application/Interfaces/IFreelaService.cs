using MyFreelas.Application.Dtos.Freela;
using MyFreelas.Domain.Pagination;

namespace MyFreelas.Application.Interfaces;

public interface IFreelaService
{
    Task<ResponseFreelaJson> RegisterFreelaAsync(RequestRegisterFreelaJson request); 

    Task<List<ResponseAllFreelasJson>> GetAllAsync( 
        RequestGetFreelaJson request, PaginationParameters paginationParameters); 

    Task<ResponseFreelaJson> GetByIdAsync(string fHashId);  

    Task DeleteAsync(string fHashId); 

    Task UpdateAsync(string fHashId, RequestUpdateFreelaJson request);   
}
